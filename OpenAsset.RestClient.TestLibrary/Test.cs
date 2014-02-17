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
			this.TestCategory();
			
			this.Test<CopyrightHolder>("CopyrightHolder", SetupCopyrightHolder, ModifyCopyrightHolder, CompareCopyrightHolder, true, false);
			this.Test<CopyrightPolicy>("CopyrightPolicy", SetupCopyrightPolicy, ModifyCopyrightPolicy, CompareCopyrightPolicy, true, false);
			this.Test<Field>("Field", SetupField, ModifyField, CompareField, false, true);
			this.Test<Photographer>("Photographer", SetupPhotographer, ModifyPhotographer, ComparePhotographer, true, true);
		}

		// Generic test for Nouns that support all verbs
		void Test<T>(string testName, Func<T> setup, Func<T,T> modify, Func<T, T, bool> compare, bool post, bool delete) 
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
					T postInstance = this.conn.SendObject<T>(newInstance, true);
					newInstance.Id = postInstance.Id;
				}
				else
				{
					newInstance = startList[0];
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
		 * Below are all helper functions used for the generic Test function
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
			item.Name = "RESTClient Test CopyrightHolder - PUT Test #" + item.UniqueCode;
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
			item.Name = "RESTClient Test CopyrightPolicy - PUT Test #" + item.UniqueCode;
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
			item.Name = "RESTClient Test Field - PUT Test #" + item.Id;
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
			item.Name = "RESTClient Test Field - PUT Test #" + item.Id;
			return item;
		}
		bool ComparePhotographer(Photographer first, Photographer second)
		{
			if (first.Id != second.Id) return false;
			if (first.Name != second.Name) return false;
			return true;
		}
	}
}
