using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
using OpenAsset.RestClient;
using System.Diagnostics;

namespace OpenAsset.RestClient.TestLibrary
{
	class RestTest
	{
		Connection conn;
		string username;
		string password;
		string oaURL;
		bool superUser;

		[STAThread]
		static void Main() {
			RestTest test = new RestTest();
			try
			{
				System.Console.WriteLine("Begin testing..");
				test.Init();
				System.Console.WriteLine("----------------------------------end--");
			}
			catch (RESTAPIException e)
			{
				System.Console.WriteLine(e);
				System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
			}
			System.Console.ReadLine();
		}

		void Init()
		{
			this.oaURL = "http://192.168.1.139";
			this.username = "admin";
			this.password = "admin";
			this.superUser = false;

			this.TestAuth();

			this.TestGet<AccessLevel>("AccessLevel", 3);
			this.TestGet<Album>("Album", -1);
			this.TestGet<AlternateStore>("AlternateStore", -1);
			this.TestGet<AspectRatio>("AspectRatio", 6);

			this.TestCategory();

			this.Test<CopyrightHolder>("CopyrightHolder", SetupCopyrightHolder, ModifyCopyrightHolder, CompareCopyrightHolder, true, false);
			this.Test<CopyrightPolicy>("CopyrightPolicy", SetupCopyrightPolicy, ModifyCopyrightPolicy, CompareCopyrightPolicy, true, false);
			this.Test<Field>("Field", SetupField, ModifyField, CompareField, false, false);		
			this.TestFieldLookupString(1);

			this.Test<File>("File", SetupFile, ModifyFile, CompareFile, true, true, "C:\\testFile.jpg");
			
			this.Test<Keyword>("Keyword", SetupKeyword, ModifyKeyword, CompareKeyword, true, true);
			this.Test<KeywordCategory>("KeywordCategory", SetupKeywordCategory, ModifyKeywordCategory, CompareKeywordCategory, true, true);
			this.Test<Photographer>("Photographer", SetupPhotographer, ModifyPhotographer, ComparePhotographer, true, false);
			this.Test<Project>("Project", SetupProject, ModifyProject, CompareProject, true, true);		
			this.Test<ProjectKeyword>("ProjectKeyword", SetupProjectKeyword, ModifyProjectKeyword, CompareProjectKeyword, true, true);
			this.Test<ProjectKeywordCategory>("ProjectKeywordCategory", SetupProjectKeywordCategory, ModifyProjectKeywordCategory, CompareProjectKeywordCategory, true, true);

			// Need to guarantee ID
			this.TestResult(97, 0);
			this.Test<Search>("Search", SetupSearch, ModifySearch, CompareSearch, true, false);

			// We can't edit the postfix, or delete this. Need to work out a new way, or prevent creation.
			// this.Test<Size>("Size", SetupSize, ModifySize, CompareSize, true, true);

			this.TestGet<TextRewrite>("TextRewrite", 0);
			this.TestGet<User>("User", -1);

		}

		// Generic test for Nounds that only support GET
		void TestGet<T>(string testName, int numberToAssert) where T : OpenAsset.RestClient.Library.Noun.Base.BaseNoun, new()
		{
			System.Console.Write("Test - " + testName);
			try
			{
				RESTOptions<T> options = new RESTOptions<T>();
				List<T> list = this.conn.GetObjects<T>(options);
				// some sort of check
				if (numberToAssert >= 0) {
					Debug.Assert(list.Count == numberToAssert);
				}
			}
			catch (Exception e)
			{
				System.Console.WriteLine(": FAIL");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("---------------------");
				return;
			}
			System.Console.WriteLine(": OK");
		}

		// Generic test for Nouns that support all verbs
		void Test<T>(string testName, Func<T> setup, Func<T,T> modify, Func<T, T, bool> compare, bool post, bool delete, string args = "") 
			where T : OpenAsset.RestClient.Library.Noun.Base.BaseNoun, new()
		{
			System.Console.Write("Test - " + testName);
			try
			{
				RESTOptions<T> options = new RESTOptions<T>();

				// GET All
				List<T> startList = this.conn.GetObjects<T>(options);

				T newInstance = setup();
				if (post)
				{
					// POST Test
					T postInstance;
					if (testName == "File")
					{
						postInstance = this.conn.SendObject<T>(newInstance, args, true);
					} else {
						postInstance = this.conn.SendObject<T>(newInstance, true);
					}
					newInstance.Id = postInstance.Id;
				}
				else
				{
					newInstance = startList[startList.Count-1];
				}

				// Verify with a single GET
				T getInstance = this.conn.GetObject<T>(newInstance.Id, options);
				Debug.Assert(compare(newInstance, getInstance));

				if (post)
				{
					// Verify all on server side
					List<T> postList = this.conn.GetObjects<T>(options);
					Debug.Assert(postList.Count == startList.Count+1);
				}

				// PUT Test
				newInstance = modify(newInstance);
				T putInstance = this.conn.SendObject(newInstance, false);
				putInstance = this.conn.GetObject<T>(newInstance.Id, options);
				Debug.Assert(compare(getInstance, putInstance) == false);

				// DELETE Test
				if (delete)
					this.conn.DeleteObject<T>(newInstance.Id, options);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(": FAIL");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("---------------------");
				return;
			}
			System.Console.WriteLine(": OK");
		}

		void TestFieldLookupString(int id)
		{
			System.Console.Write("Test - FieldLookupString");
			try
			{
				RESTOptions<FieldLookupString> options = new RESTOptions<FieldLookupString>();
				options.AddDisplayField("field_id");
				options.SetSearchParameter("id", id.ToString());
				List<FieldLookupString> nestedFieldLookupString = this.conn.GetObjects<FieldLookupString>(id, "Fields", options);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(": Fail");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("--------------------");
			}
		}

		void TestResult(int id, int count)
		{
			System.Console.Write("Test - Result");
			try
			{
				RESTOptions<Result> options = new RESTOptions<Result>();
				List<Result> itemList = this.conn.GetObjects<Result>(id, "Searches", options);
				Debug.Assert(itemList.Count == count);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(": FAIL");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("---------------------");
			}
		}

		void TestCategory() {
			System.Console.Write("Test - Category");
			try
			{
				// GET all
				RESTOptions<Category> optionsCategory = new RESTOptions<Category>();
				List<Category> categoryList = this.conn.GetObjects<Category>(optionsCategory);
				Category category = categoryList[0];

				// PUT test, no need to retain returned object
				string newCategoryOriginalName = category.Name;
				category.Name = "REST Test Category rename";
				this.conn.SendObject<Category>(category);

				// GET to verify PUT
				Category getCategory = this.conn.GetObject<Category>(category.Id, optionsCategory);
				Debug.Assert(getCategory.Name == "REST Test Category rename");
				category.Name = newCategoryOriginalName;
				this.conn.SendObject<Category>(category);
			}	
			catch (Exception e)
			{
				System.Console.WriteLine(": FAIL");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("---------------------");
				return;
			}
			System.Console.WriteLine(": OK");
		}

		void TestAuth()
		{
			System.Console.Write("Test - Auth");
			try
			{
				// TODO: This is a precondition, needs refinement. 
				this.conn = Connection.GetConnection(this.oaURL, "anonymous", "");
				bool anonymousUser = conn.ValidateCredentials();
				Debug.Assert(anonymousUser);

				this.conn.NewSession("foo", "bar");
				bool invalidUser = conn.ValidateCredentials();
				Debug.Assert(invalidUser == false);

				this.conn.NewSession(this.username, this.password);
				bool validUser = conn.ValidateCredentials();
				Debug.Assert(validUser);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(": FAIL");
				System.Console.WriteLine("Error:");
				System.Console.WriteLine(e);
				System.Console.WriteLine("---------------------");
				return;
			}
			System.Console.WriteLine(": OK");
		}

		/* 
		 * Below are all helper functions used for the generic Test function.
		 * Possibly refactor these into each Noun.
		 * 
		 */
		// CopyrightHolder Generics
		CopyrightHolder SetupCopyrightHolder()
		{
			CopyrightHolder item = new CopyrightHolder();
			item.Name = "RESTClient Test CopyrightHolder";
			return item;
		}
		CopyrightHolder ModifyCopyrightHolder(CopyrightHolder item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareCopyrightHolder(CopyrightHolder first, CopyrightHolder second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// CopyrightPolicy Generics
		CopyrightPolicy SetupCopyrightPolicy()
		{
			CopyrightPolicy item = new CopyrightPolicy();
			item.Name = "RESTClient Test CopyrightPolicy";
			return item;
		}
		CopyrightPolicy ModifyCopyrightPolicy(CopyrightPolicy item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareCopyrightPolicy(CopyrightPolicy first, CopyrightPolicy second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// Field Generics
		Field SetupField()
		{
			Field item = new Field();
			item.Name = "RESTClient Test Field";
			item.Description = "This is a test field created by OpenAsset.RestClient.TestLibrary.";
			item.FieldType = "image";
			item.FieldDisplayType = "date";
			return item;
		}
		Field ModifyField(Field item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareField(Field first, Field second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// Photographer Generics
		Photographer SetupPhotographer()
		{
			Photographer item = new Photographer();
			item.Name = "RESTClient Test Photographer";
			return item;
		}
		Photographer ModifyPhotographer(Photographer item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool ComparePhotographer(Photographer first, Photographer second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// Project Generics
		Project SetupProject()
		{
			Project item = new Project();
			item.Name = "RESTClient Test Project";
			item.Code = "RST001";
			return item;
		}
		Project ModifyProject(Project item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			item.Code = "RST" + item.Id;
			return item;
		}
		bool CompareProject(Project first, Project second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}
		
		// Size Generics
		Size SetupSize()
		{
			Size item = new Size();
			item.Name = "RESTClient Test Size";
			item.Postfix = "rst";
			item.FileFormat = "jpg"; // options: bmp, gif, jpg, png, tif, tiff
			item.Colourspace = "rgb";
			item.Width = 250;
			item.Height = 250;
			item.AlwaysCreate = false;
			item.XResolution = 72;
			return item;
		}
		Size ModifySize(Size item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareSize(Size first, Size second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// KeywordCategory Generics
		KeywordCategory SetupKeywordCategory()
		{
			KeywordCategory item = new KeywordCategory();
			item.CategoryId = 1; // ensure?
			item.Name = "RESTClient Test KeywordCategory";
			return item;
		}
		KeywordCategory ModifyKeywordCategory(KeywordCategory item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareKeywordCategory(KeywordCategory first, KeywordCategory second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// Keywords
		Keyword SetupKeyword()
		{
			Keyword item = new Keyword();
			item.Name = "RESTClient Test Keyword";
			item.KeywordCategoryId = 1; // ensure?
			return item;
		}
		Keyword ModifyKeyword(Keyword item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareKeyword(Keyword first, Keyword second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// ProjectKeywordCategory Generics
		ProjectKeywordCategory SetupProjectKeywordCategory()
		{
			ProjectKeywordCategory item = new ProjectKeywordCategory();
			item.Name = "RESTClient Test ProjectKeywordCategory";
			return item;
		}
		ProjectKeywordCategory ModifyProjectKeywordCategory(ProjectKeywordCategory item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareProjectKeywordCategory(ProjectKeywordCategory first, ProjectKeywordCategory second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}
		
		// ProjectKeywords Generics
		ProjectKeyword SetupProjectKeyword()
		{
			ProjectKeyword item = new ProjectKeyword();
			item.Name = "RESTClient Test ProjectKeyword";
			item.ProjectKeywordCategoryId = 1;
			return item;
		}
		ProjectKeyword ModifyProjectKeyword(ProjectKeyword item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareProjectKeyword(ProjectKeyword first, ProjectKeyword second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}

		// File Generics
		File SetupFile()
		{
			File item = new File();
			// Need a way to verify this. Perhaps storing an image as base64 and then writing it out to a temp
			// dir and then loading that. For now use a static path.
			item.Filename = "C:\\testFile.jpg";
			item.OriginalFilename = "testFile.jpg";
			item.AccessLevel = 1;
			item.Rank = 1;
			item.CategoryId = 2; // Reference, need to get this properly, perhaps with a GET
			item.ProjectId = 0;
			return item;
		}
		File ModifyFile(File item)
		{
			item.AccessLevel = 2;
			return item;
		}
		bool CompareFile(File first, File second)
		{
			if (first.AccessLevel != second.AccessLevel) return false;

			return true;
		}

		// Search Generics
		Search SetupSearch()
		{
			SearchItem searchItems = new SearchItem();
			// setup searchItems
			searchItems.Code = "popularFields";
			searchItems.Values = new List<string>();
			searchItems.Values.Add("testFile.jpg");

			// setup the actual Search
			Search item = new Search();
			item.Name = "RESTClient Test Search";
			item.SearchItems = new List<SearchItem>();
			item.SearchItems.Add(searchItems);
			item.Saved = true;
			return item;
		}
		Search ModifySearch(Search item)
		{
			item.Name = item.Name + " - PUT Test #" + item.Id;
			return item;
		}
		bool CompareSearch(Search first, Search second)
		{
			if (first.Name != second.Name) return false;
			return true; 
		}
	}
}
