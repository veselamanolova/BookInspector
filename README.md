# BookInspector

**Team name: Sea Wolfs**

**Team Leader: Anastas Kosstow**

**First lieutenant, CFO, CTO, HR and PR manager: Vesela Manolova**

**Team members: Anastas Kosstow, Vesela Manolova**

**Project: Book database**


**Application Features**: 
**The application will consist of the following elements: **

The **book** should have the following fields: 
* ID
* Title 
* List of Authors: The book can have one or more authors.  
* Date 
* Publisher 
* ISBN - consisting of 13 digits
* Pages 
* Description 
* List of Users who has added this book to their favourite books. 

**The Author should have the following fields:**
* ID
* Name 
* List of books. An author could have 1 or more books. 

**The Publisher should have the following fields:**
* ID
* Name 
* List of Books

**The User should have the following fields:**
* ID
* Name 

**The User can give a rating to a book**


****It will have the following functionality:****

**Import Books from JSON:**
* file and 
* www.googleapis.com/books/

* The URL should be in the following format: 
* https://www.googleapis.com/books/v1/volumes?q=[serch_string]&maxResults=[returned_books_numbers]
* Example URLs: 
* https://www.googleapis.com/books/v1/volumes?q=AngularJS&maxResults=2
* https://www.googleapis.com/books/v1/volumes?q=ReactJS&maxResults=5
* https://www.googleapis.com/books/v1/volumes?q=typescript&maxResults=5


**Add:**
* Books, 
* Authors, 
* Publishers 
* Users
* Category

**Edit:**
* Users

**List:**
* All Authors
* Publishers
* Users
* Categories

**Search for a books or list of books** by:
* Title,
* Author 

**Board Link:**
https://dbprojecct.visualstudio.com/DBProject/_sprints/taskboard/DBProject%20Team/DBProject/Sprint%201
