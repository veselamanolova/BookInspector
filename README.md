# BookInspector

**Team name: Sea Wolfs**

**Team Leader: Anastas Kosstow**

**First lieutenant, CFO, CTO, HR and PR manager: Vesela Manolova**

**Team members: Anastas Kosstow, Vesela Manolova**

**Project: Book database**

**Board Link:**
https://dbprojecct.visualstudio.com/DBProject/_sprints/taskboard/DBProject%20Team/DBProject/Sprint%201


**Second sprint - Web Application Features**


1. **Common Layout will consist of:** 

    * **Menu tabs visible to all users:** 

        * Home
        * Books
        * Categories 
        * Login tab  -  shows as login when the user is not logged in and Welcome, username when the user is logged in    
        

    *  **Menu tab visble only to logged in users:**

        * Community - it will be visible for logged in users  

2. **Home:** 
    * **There will be a global search book on the top**
    * **In the middle of the page it will list tob 5 books by rating**
    
3. **Books:** 
    * **There will be a search book tab on the top**
    * **In the middle of the page it will list books. It will show for ach book the following information:**
        * Title,
        * Image,
        * Description, 
        * Rating, 
        * Button Details which will redirect to Book page and
        * Rating control which will save a rating to the book if the user is loggged in. If not it will be redirected to log it first. 

4. **Book:** 
    * It will show the full information about the book 
    * And there will be a button to read part of the book online


5.  **Category Page**
    * **There will be a search books by category tab on the top**
    * **In the middle of the page all categories will be listes. It will show for each category the following information:**
        * Category Name,
        * View all books button - which will redirect to Books page but will list only the books from the selected category

6.  **Community**
    * **This page will be visible only to the logged in users**
    * It will list all Users and the books they liked




**First Sprint**: 

**Console Application Features**: 

**The application will consist of the following elements:**
    
**The **book** should have the following fields:**
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

The URL should be in the following format: 
https://www.googleapis.com/books/v1/volumes?q=[serch_string]&maxResults=[returned_books_numbers]

Example URLs: 
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





