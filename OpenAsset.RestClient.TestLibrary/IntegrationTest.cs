using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
using OpenAsset.RestClient.Library.Noun.Base;
using OpenAsset.RestClient;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;

// Class tests the integration of individual nouns
namespace OpenAsset.RestClient.TestLibrary
{
    class IntegrationTest
    {
        Connection conn;
        public string username;
        public string password;
        public string test_id;

        string _oaURL;
        public string oaURL
        {
            get { return _oaURL; }
            set
            {
                if (!IsUrlValid(value))
                    throw new Exception("URL format not supported");
                _oaURL = value;
            }
        }

        [STAThread]
        public static void Main()
        {
            string oaURL = "http://192.168.4.85";
            string username = "admin";
            string password = "admin";

            IntegrationTest test = new IntegrationTest(oaURL, username, password);

            try
            {
                Console.WriteLine("Test " + test.test_id + " Begin:\n\n");
                test.init();
                Console.WriteLine("\n\nTest " + test.test_id + " End");
            }
            catch (RESTAPIException e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
            Console.ReadLine();
        }

        // Default constructor
        public IntegrationTest()
        {
            this.oaURL = "";
            this.username = "";
            this.password = "";
            this.test_id = Guid.NewGuid().ToString();
        }

        // Overloaded constructor
        public IntegrationTest(string oaURL = "", string username = "", string password = "")
        {
            this.oaURL = oaURL;
            this.username = username;
            this.password = password;
            this.test_id = Guid.NewGuid().ToString();

            if (!EstablishConnection())
            {
                throw new Exception("Incorrect credentials!");
            }
        }

        // Makes a connection to OpenAsset
        public bool EstablishConnection()
        {
            this.conn = Connection.GetConnection(this.oaURL, this.username, this.password);
            return conn.ValidateCredentials();
        }

        // Initiates the test
        public void init()
        {
            Category projectCategory = GetProjectCategoryId();

            ProjectKeywordCategory projectKeywordCategory = CreateProjectKeywordCategory();
            ProjectKeyword projectKeyword = CreateProjectKeyword(projectKeywordCategory.Id);

            Field projectField = CreateField("project");
            Field imageField = CreateField("image");

            Project project = CreateProject(projectKeyword, projectField);

            KeywordCategory keywordCategory = CreateKeywordCategory(projectCategory.Id);
            Keyword keyword = CreateKeyword(keywordCategory.Id);

            Photographer photographer = CreatePhotographer();
            AccessLevel accessLevel = GetRandomAccessLevel();

            CopyrightPolicy copyrightPolicy = CreateCopyrightPolicy();
            CopyrightHolder copyrightHolder = CreateCopyrightHolder(copyrightPolicy.Id);

            File file = UploadFile(projectCategory, project, imageField, keyword, photographer, accessLevel, copyrightHolder);

            Album album = CreateAlbum(file);

            List<Search> searches = CreateSearches(file, keywordCategory, keyword, imageField, photographer, copyrightHolder, accessLevel, project, album);

            BaseNoun[] nouns = new BaseNoun[] { file, photographer, copyrightHolder, copyrightPolicy, album, keyword, keywordCategory, imageField, projectField, projectKeyword, projectKeywordCategory, project };
            VerifySearches(searches, file);
            //if (VerifySearches(searches, file))
            //{
             //   DeleteNoun(nouns);
            //}
        }

        // Returns the ID of the first Project category
        public Category GetProjectCategoryId()
        {
            Console.Write("Getting Project Category: ");
            List<Category> categoryList = this.conn.GetObjects<Category>(new RESTOptions<Category>());
            foreach (Category category in categoryList)
            {
                if (category.ProjectsCategory == 1)
                {
                    Console.WriteLine(category.Id);
                    return category;
                }
            }
            throw new Exception("Project Category does not exist!");
        }

        // Creates a project
        public Project CreateProject(ProjectKeyword projectKeyword, Field projectField)
        {
            Console.Write("Creating Project: ");
            Project projectItem = new Project();
            projectItem.Name = this.test_id + "_Test_Project";
            projectItem.Code = this.test_id + ".00";

            List<ProjectKeyword> projectKeywordList = new List<ProjectKeyword>();
            projectKeywordList.Add(projectKeyword);
            projectItem.ProjectKeywords = projectKeywordList;

            List<Field> projectFieldList = new List<Field>();
            projectFieldList.Add(projectField);
            projectItem.Fields = projectFieldList;

            Project resp = this.conn.SendObject<Project>(projectItem, true);

            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Project Keyword Category
        public ProjectKeywordCategory CreateProjectKeywordCategory()
        {
            Console.Write("Creating Project Keyword Category: ");
            ProjectKeywordCategory projectKeywordCategoryItem = new ProjectKeywordCategory();
            projectKeywordCategoryItem.Name = this.test_id;
            ProjectKeywordCategory resp = this.conn.SendObject<ProjectKeywordCategory>(projectKeywordCategoryItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Project Keyword
        public ProjectKeyword CreateProjectKeyword(int projectKeywordCategoryId)
        {
            Console.Write("Creating Project Keyword: ");
            ProjectKeyword projectKeywordItem = new ProjectKeyword();
            projectKeywordItem.Name = this.test_id;
            projectKeywordItem.ProjectKeywordCategoryId = projectKeywordCategoryId;
            ProjectKeyword resp = this.conn.SendObject<ProjectKeyword>(projectKeywordItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Keyword Category
        public KeywordCategory CreateKeywordCategory(int categoryId)
        {
            Console.Write("Creating Keyword Category: ");
            KeywordCategory keywordCategoryItem = new KeywordCategory();
            keywordCategoryItem.CategoryId = categoryId;
            keywordCategoryItem.Name = this.test_id;
            KeywordCategory resp = this.conn.SendObject<KeywordCategory>(keywordCategoryItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Keyword
        public Keyword CreateKeyword(int projectKeywordCategoryId)
        {
            Console.Write("Creating Keyword: ");
            Keyword keywordItem = new Keyword();
            keywordItem.KeywordCategoryId = projectKeywordCategoryId;
            keywordItem.Name = this.test_id;
            Keyword resp = this.conn.SendObject<Keyword>(keywordItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a image/project type Field
        public Field CreateField(string fieldType)
        {
            Console.Write("Creating " + fieldType + " Field: ");
            Field fieldItem = new Field();
            fieldItem.Name = Guid.NewGuid().ToString();
            fieldItem.Description = this.test_id + "_Field_Test";
            fieldItem.FieldType = fieldType;
            fieldItem.FieldDisplayType = "singleLine";
            fieldItem.IncludeOnSearch = true;
            
            Field resp = this.conn.SendObject<Field>(fieldItem, true);
            List<String> valueList = new List<String>();
            valueList.Add(this.test_id);
            resp.Values = valueList;

            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Photographer
        public Photographer CreatePhotographer()
        {
            Console.Write("Creating Photographer: ");
            Photographer photographerItem = new Photographer();
            photographerItem.Name = this.test_id;
            Photographer resp = this.conn.SendObject<Photographer>(photographerItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Copyright Policy
        public CopyrightPolicy CreateCopyrightPolicy()
        {
            Console.Write("Creating Copyright Policy: ");
            CopyrightPolicy copyrightPolicyItem = new CopyrightPolicy();
            copyrightPolicyItem.Name = this.test_id;
            copyrightPolicyItem.Description = this.test_id + "_Test_Copyright_Policy";
            CopyrightPolicy resp = this.conn.SendObject<CopyrightPolicy>(copyrightPolicyItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Creates a Copyright Holder
        public CopyrightHolder CreateCopyrightHolder(int copyrightPolicyId)
        {
            Console.Write("Creating Copyright Holder: ");
            CopyrightHolder copyrightHolderItem = new CopyrightHolder();
            copyrightHolderItem.Name = this.test_id;
            copyrightHolderItem.CopyrightPolicyId = copyrightPolicyId;
            CopyrightHolder resp = this.conn.SendObject<CopyrightHolder>(copyrightHolderItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Returns a random Access Level from OpenAsset
        public AccessLevel GetRandomAccessLevel()
        {
            Console.Write("Getting Random Access Level: ");
            List<AccessLevel> accessLevelList = this.conn.GetObjects<AccessLevel>(new RESTOptions<AccessLevel>());
            Random rand = new Random();
            int randIndex = rand.Next(accessLevelList.Count);
            AccessLevel accessLevel = accessLevelList[randIndex];
            Console.WriteLine(accessLevel.Id);
            return accessLevel;
        }

        // Creates an Album
        public Album CreateAlbum(File file)
        {
            Console.Write("Creating Album: ");
            Album albumItem = new Album();
            albumItem.Name = this.test_id;
            albumItem.Description = this.test_id + "_Test_Album";
            albumItem.ShareWithAllUsers = true;
            albumItem.AllUsersCanModify = true;
            albumItem.CompanyAlbum = true;

            List<File> fileList = new List<File>();
            fileList.Add(file);
            albumItem.Files = fileList;
            
            Album resp = this.conn.SendObject<Album>(albumItem, true);
            Console.WriteLine(resp.Id);
            return resp;
        }

        // Generates and uploads a File to OpenAsset, along with tagging it's field, keyword, photographer, access level, and copyright holder
        public File UploadFile(Category category, Project project, Field field, Keyword keyword, Photographer photographer, AccessLevel accessLevel, CopyrightHolder copyrightHolder)
        {
            string filename = this.test_id;
            string filepath = GenerateImage(filename);

            List<Keyword> keywordList = new List<Keyword>();
            keywordList.Add(keyword);

            List<Field> fieldList = new List<Field>();
            fieldList.Add(field);

            Console.Write("Uploading File: ");
            File fileItem = new File();
            fileItem.Filename = filepath;
            fileItem.OriginalFilename = filename + ".png";

            fileItem.Caption = this.test_id + "_Test_Caption";
            fileItem.Description = this.test_id + "_Test_Description";

            fileItem.CategoryId = category.Id;
            fileItem.ProjectId = project.Id;

            fileItem.Fields = fieldList;
            fileItem.Keywords = keywordList;

            fileItem.CopyrightHolderId = copyrightHolder.Id;
            fileItem.PhotographerId = photographer.Id;

            fileItem.AccessLevel = accessLevel.Id;
            fileItem.Rank = 1;

            File resp = this.conn.SendObject<File>(fileItem, filepath, true);

            Console.WriteLine(resp.Id);
            DeleteImageFile(filepath);
            return resp;
        }

        public List<Search> CreateSearches(File file, KeywordCategory keywordCategory, Keyword keyword, Field field, Photographer photographer, CopyrightHolder copyrightHolder, AccessLevel accessLevel, 
            Project project, Album album)
        {
            Console.WriteLine("Creating Searches");
            Search search;
            SearchItem searchItems;
            List<Search> searchList = new List<Search>();

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Filename";
            searchItems.Code = "filename";
            searchItems.Exclude = false;
            searchItems.Values = new List<String>();
            searchItems.Values.Add(file.Filename);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Original_Filename";
            searchItems.Code = "originalFilename";
            searchItems.Exclude = false;
            searchItems.Values = new List<String>();
            searchItems.Values.Add(file.OriginalFilename);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Caption";
            searchItems.Code = "caption";
            searchItems.Exclude = false;
            searchItems.Values = new List<String>();
            searchItems.Values.Add(file.Caption);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Description";
            searchItems.Code = "description";
            searchItems.Exclude = false;
            searchItems.Values = new List<String>();
            searchItems.Values.Add(file.Description);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Photographer";
            searchItems.Code = "photographer";
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(photographer.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Keyword";
            searchItems.Code = "keyword."+keywordCategory.Id;
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(keyword.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Project";
            searchItems.Code = "project";
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(project.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Album";
            searchItems.Code = "album";
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(album.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_CopyrightHolder";
            searchItems.Code = "copyrightHolder";
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(copyrightHolder.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            search = new Search();
            searchItems = new SearchItem();
            search.Saved = false;
            search.Name = this.test_id + "_Test_Search_Field";
            searchItems.Code = "field."+field.Id;
            searchItems.Exclude = false;
            searchItems.Ids = new List<int>();
            searchItems.Ids.Add(field.Id);
            search.SearchItems.Add(searchItems);
            searchList.Add(search);

            return searchList;
        }

        // Verifies that each search brought a result that includes the uploaded file
        public bool VerifySearches(List<Search> searchList, File file) {
            Console.WriteLine("Verifying Searches:");
            RESTOptions<Result> options;
            List<Result> itemList;
            Search currSearch;

            bool fileFound = false;

            foreach (Search search in searchList)
            {
                currSearch = this.conn.SendObject<Search>(search, true);
                options = new RESTOptions<Result>();
                itemList = this.conn.GetObjects<Result>(currSearch.Id, "Searches", options);
                fileFound = false;

                foreach (Result result in itemList)
                {
                    if (result.FileId == file.Id)
                    {
                        fileFound = true;
                        break;
                    }
                }

                if (!fileFound)
                {
                    throw new Exception("File search not found for " + currSearch.Code + " (" + currSearch.Id + ")");
                }
            }
         
            return true;
        }

        // Deletes a Noun
        public bool DeleteNoun(BaseNoun[] nouns)
        {
            Console.WriteLine("Deleting Objects:");
            try
            {
                foreach (BaseNoun noun in nouns)
                {
                    this.conn.DeleteObject<BaseNoun>(noun.Id);
                    Console.WriteLine("\tDeleted "+noun.GetType().Name);
                }
            }
            catch (RESTAPIException e) {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            return true;
        }

        // Generates an image from text
        public string GenerateImage(string filename)
        {
            Font font = new Font("Arial", 20);
            Color textColor = Color.FromName("black");
            Color backColor = Color.FromName("white");
            Image img = DrawText(filename, font, textColor, backColor);

            string filepath = "C:\\" + filename + ".png";
            img.Save(filepath, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("Generated temp image");
            return filepath;
        }

        // Deletes a file at specified path
        public bool DeleteImageFile(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
                Console.WriteLine("Deleted generated temp image");
                return true;
            }
            return false;
        }

        // Creates and retuns an image object
        private Image DrawText(string text, Font font, Color textColor, Color backColor)
        {
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            SizeF textSize = drawing.MeasureString(text, font);

            img.Dispose();
            drawing.Dispose();

            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);

            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        // Validates a url schema
        private bool IsUrlValid(string url)
        {
            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }
}