openasset-rest-cs
=================

C# library &amp; sample code for using the OpenAsset REST API

### Intro

In 2013 Axomic will release a REST API for OpenAsset.

To accompany this, a set of client libraries are freely available to downloaded and use. The REST API is still under development and will innevitibly change during 2013 before a final release is announced, the features frozen and back compatibility guaranteed.

### REST API Endpoint Docs

The documentation for the REST API endpoints used by this library can be found at:

http://help.axomic.com/07_Technical_Stuff/APIs/REST

### Usage example

```csharp
// Example for noun in http://localhost/REST/File
string baseURL = "http://localhost";
string password = "password";
string username = "username";

// create the object with the File noun methods
FileNoun fileNoun = new FileNoun(baseURL, username, password);

// Get an array with a set of Files            
int limit  = 10; // amount limit for the results
int offset = 0; // offset of results for pagination
bool forceHTTPRequest = true; // should make a new request or use the cached result?
FileObject[] resultArray = fileNoun.getNounObjects( limit, offset, forceHTTPRequest);

// Update caption of a single File object        
int fileId = 123;
string caption = "New caption";   
FileObject resultObj = fileNoun.getNounObjectById(fileId);
resultObj.Caption = caption;
int putResponseCode = fileNoun.putNounObjects(new FileObject[] {resultObj});
```

### Find out more

Please get in contact if you're interested in knowing more

- https://twitter.com/OpenAsset
- http://www.axomic.com

### License

The MIT License (MIT)
Copyright (c) 2013 Axomic Ltd

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