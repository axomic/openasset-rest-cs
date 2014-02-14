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
		ConnectionHelper conn;

		[STAThread]
		static void Main() {
			RestTest test = new RestTest();
			try
			{
				System.Console.WriteLine("Begin testing..");
				test.Init();
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
			string oaURL = "http://192.168.1.139";
			string username = "admin";
			string password = "admin";

			/* TODO: This is a precondition, needs refinement. 
			System.Console.WriteLine("Test - Anonymous User");
			this.conn = ConnectionHelper.GetConnectionHelper(oaURL, "anonymous", "");
			bool anonymousUser = conn.ValidateCredentials();
			Debug.Assert(anonymousUser);
			*/

			System.Console.WriteLine("Test - Invalid User");
			conn.NewSession("foo", "bar");
			bool invalidUser = conn.ValidateCredentials();
			Debug.Assert(invalidUser == false);
			

			System.Console.WriteLine("Test - Valid User");
			conn.NewSession(username, password);
			bool validUser = conn.ValidateCredentials();
			Debug.Assert(validUser);

			System.Console.WriteLine("Test - Category");
			this.TestCategory();
		}

		void TestCategory() {
			// GET all
			RESTOptions<Category> optionsCategory = new RESTOptions<Category>();
			List<Category> startCategoryList = this.conn.GetObjects<Category>(optionsCategory);
			// TODO: check if we can test Failed on all


			// POST a new category
			Category newCategory = new Category();
			newCategory.Name = "RestClient Test Category";
			newCategory.Code = "RST001";
			newCategory.Description = "This is a test category created by the RestClient TestLibrary";
			Category returnedCategory = this.conn.SendObject<Category>(newCategory, true);

			// GET one
			Category getNewCategory = this.conn.GetObject<Category>(returnedCategory.Id, optionsCategory);
			Debug.Assert(getNewCategory.Failed == false);

			// Validate object
			Debug.Assert(getNewCategory.Id == returnedCategory.Id);
			Debug.Assert(getNewCategory.Name == newCategory.Name);
			Debug.Assert(getNewCategory.Code == newCategory.Code);
			Debug.Assert(getNewCategory.Description == newCategory.Description);

			// Validate count
			List<Category> newCategoryList = this.conn.GetObjects<Category>(optionsCategory);
			Debug.Assert(newCategoryList.Count == startCategoryList.Count);
		}
	}
}
