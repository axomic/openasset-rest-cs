# openasset-rest-cs
C# library &amp; sample code for using the OpenAsset REST API v1

## Documentation
The documentation for the REST API endpoints used by this library can be found at:
http://help.openasset.com/06_Integration/REST_API

### Sample Usage
The examples below show how to establish a connection to OpenAsset and get/send data.

```csharp
// import library directives
using OpenAsset.RestClient;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
```

#### Establish Connection to OpenAsset
```csharp
string baseURL = "http://localhost";
string password = "password";
string username = "username";

// Establish a connection to OpenAsset
Connection conn = Connection.GetConnection(baseURL, username, password);

// Verify credentials
bool isCorrectCredentials = conn.ValidateCredentials();
if (isCorrectCredentials)
{
    Console.WriteLine("Correct Credentials");
}
else
{
    Console.WriteLine("Incorrect Credentials");
}
```

#### Sample Code to Get Nouns
```csharp
// Create a RestOptions object to filter search
RESTOptions<File> options = new RESTOptions<File>();

// Adding filter for rank
options.SetSearchParameter("rank", "5");

// Setting a limit of 10 files
options.Limit = 10;

// Create a List of Files to contain the response of file objects
List<File> fileList = conn.GetObjects<File>(options);

// Iterate over the list and print the filenames of the files
foreach (File fileObj in fileList) {
    Console.WriteLine(fileObj.Filename);
}
```

#### Sample Code to Create a Noun
```csharp
String filename = "test.jpg";
String filepath = "C:\\test.jpg";

// Create a new file object
File fileItem = new File();

// Add file details
fileItem.OriginalFilename = filename;
fileItem.CategoryId = 2;

// Make a POST request by setting the last parameter as TRUE
File responseFile = conn.SendObject<File>(fileItem, filepath, true);

// Get the id of the newly created file
Console.WriteLine(responseFile.Id);
```

#### Sample Code to Update a Noun
```csharp
// Create a new file object
File fileItem = new File();

// Set the id to an existing image in OpenAsset
fileItem.Id = 254;

// Updating its description field
fileItem.Description = "Updated Description!!";

// Make a PUT request by setting the last parameter as FALSE
File responseFile = conn.SendObject<File>(fileItem, false);

// Print the description of the updated file
Console.WriteLine(responseFile.Description);
```

#### Sample Code to Delete a Noun
```csharp
// Create a new file object
File fileItem = new File();

// Set the id to an existing image in OpenAsset
fileItem.Id = 254;

// Send the File object to the DeleteObject method
conn.DeleteObject(noun);

Console.WriteLine("File deleted!");
```

#### Integration of Nouns
You can also integrate individual nouns with each other. For example, the below code integrates an Album noun with the File noun. You can check out the IntegrationTest.cs file inside the TestLibrary for more examples of noun integration.
```csharp
// Establish connection to OpenAsset and validate credentials
Connection conn = Connection.GetConnection("http://192.168.4.85", "username", "password");
conn.ValidateCredentials();

// Create a new File object and set its id to an existing file in OpenAsset
File fileItem = new File();
fileItem.Id = 209;

// Create a new Album Object
Album albumItem = new Album();

// Set its properties
albumItem.Name = "Test Album";
albumItem.Description = "This is a test album";
albumItem.CompanyAlbum = true;

// Add the file object to this album
albumItem.Files.Add(fileItem);

// Make a POST request to create the album and add the file to it
Album responseAlbum = conn.SendObject<Album>(albumItem, true);

// Print the id of the newly created album
Console.WriteLine("New Album Id: " + responseAlbum.Id);

// Iterate over all the files inside the album and print their ids
List<File> fileList = responseAlbum.Files;
foreach (File file in fileList)
{
    Console.WriteLine(file.Id);
}
```


### Find out more

Please get in contact if you're interested in knowing more

- https://twitter.com/OpenAsset
- http://www.openasset.com

### License

The MIT License (MIT)
Copyright (c) 2013-2015 Axomic Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Third party libraries:

### Json.NET Library (http://json.codeplex.com/)
####License:

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person obtaining a copy of this
software and associated documentation files (the "Software"), to deal in the Software
without restriction, including without limitation the rights to use, copy, modify,
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
