### Create ASP.NET MVC Web Application From Scratch
##### Create Domain Models (Blog Post & Tag)
1. Create `BlogPost.cs` and `Tag.cs` in `Models > Domain`
2. Create props for domain models
3. Use `ICollection` to declare many-to-many relation
##### Adding Entity Framework Core Packages
4. In `Dependencies > Manage NuGet package`, install
	1. `Microsoft.EntityFrameworkCore.SqlServer`
	2. `Microsoft.EntityFrameworkCore.Tools`
##### Creating the DbContext Class
5. Create `BlogDbContext.cs` (inherited from `DbContext`) in `Data`
6. Create constructor
7. Create `DbSet` props for table creation
##### Adding ConnectionString To The Database in Appsetting.Json
8. Add `ConnectionStrings` in `appsettings.json`
##### Injecting DbContext Into Our Application
9. Inject `BlogDbContext`in `Program.cs
##### Run Migrations
10. Run `Add-Migration <Migration Name>` in PM console
11. Run `Update-Database` in PM console
### Create Tag CRUD Operations in ASP.NET MVC
##### Changes Basic UI For our Application
12. Modify bootstrap class in `Views > Shared > _Layout.cshtml`
13. Create dropdown menu in the nav-bar, `Admin > Add Tag`
##### Create New Controller (AdminTags), Add Action Method (GET) and Create New View
14. Create new controller `AdminTagsController.cs`
15. Create Add action method (GET)
16. Create view `Add.cshtml` and header
17. Replace`href` by `asp` attributes to navigate Add action method
##### Create New HTML Form to Add New Tag
18. Create `form` in `Add.cshtml` which includes:
	1. `Name` input field
	2. `Display` input field
	3. Submit button
##### Data Binding in ASP.NET
19. Create view model `AddTagRequest.cs` in `Models > View`
20. Create Add action method (POST)
21. Declare `@model` in `Add.cshtml`
22. Data binding using `asp-for`
##### Form Submission and Saving Data To Database using DbContext
23. Map view model instance (`addTagRequest`) to domain model instance (`tag`)
24. Create constructor which takes `BlogDbContext` as param
25. Add `tag` into database and save changes
##### Display All Tags (Get All Tags)
26. Create List action method (GET)
27. Create view `List.cshtml` and header
28. Get all tags through `blogDbContext` and pass them to view
29. Declare `@model` for `List.cshtml`
30. Conditional render table to display tags
31. Add dropdown option to navigate List action method
##### Update Tag Functionality
32. Add `Edit` table column in `List.cshtml`(using `asp` attributes to navigate)
33. Create Edit action method (GET)
34. Create view `Edit.cshtml` and header
35. Get `tag` by `Id`
36. Create view model `EditTagRequest.cs` in `Models > View`
37. Map domain model instance (`tag`) to view model instance (`EditTagRequest`) if `tag` exists, and pass the view model instance to view
38. Declare `@model` for `Edit.cshtml`
39. Create `form` in `Edit.cshtml` which includes: (conditional render if `Model` is not empty)
	1. `Id` input field with data binding (read only)
	2. `Name` input field with data binding
	3. `Display` input field with data binding
	4. Submit button
40. Create Edit action method (POST)
41. Get domain model instance `tag` by `id`
42. Update `tag` using `editTagRequest` props and save changes
43. Redirect to List action if success, otherwise back to Edit  action
##### Delete Tag Functionality
44. Create delete button next to update button in `Edit.cshtml` (with `asp` navigation)
45. Create Delete action method (POST) 
46. Get domain model instance `tag` by `id`
47. Remove tag if it exists in database
### Asynchronous Programming and Repository Pattern
##### Making our methods Asynchronous
48. Implement `await` and `Async` to all action methods (wrap `Task` to return type)
##### Implementation of Repository Pattern
49. Create interface`ITagRepository.cs` in `Repositories`
50. Create CRUD abstract methods
51. Create class `TagRepository.cs`(implements `ITagRepository`) in `Repositories`
52. Create constructor which takes `BlogDbContext` as param
53. Migrate code from controller to repo
54. Inject `ITagRepository` into `Program.cs`
55. Modify param of controller constructor from `BlogDbContext` to `ITagRepository`
56. Switch all `BlogDbContext` methods to `TagRepo` methods

### BlogPost CRUD Operations - Dropdowns, Checkbox, DatePickers etc
##### Create New Controller (AdminBlogs), Add Action Method (GET) and Create New View
57. Create new controller `AdminBlogPostsController.cs`
58. Create Add action method (GET)
59. Create view `Add.cshtml` and header
60. Add dropdown option to navigate Add action method
##### Create New HTML Form to Add New Blog
61. Create `form` in `Add.cshtml` which includes all props from `BlogPost.cs`, below shows special input type (with `asp` data binding for the view model created next step):
	1. `Content`: `textarea`
	2. `PublishedDate`: `date`
	3. `Visible`: `checkbox`
62. Create view model `AddBlogPostRequest.cs` with all props from `BlogPost.cs` (except `Id` and `Tags`)
63. Declare `@model` for `Add.cshtml`
64. Create two extra props  in `AddBlogPostRequest.cs`:
	1. `Tags`: `IEnumerable<SelectListItem>` 
	2. `SelectedTag`: `string`
65. Create constructor in `AdminBlogPostsController.cs` which takes `ITagRepository` as param
66. In Add action method (GET), we plan to render `Add.cshtml` view, where user can select tags among **ALL TAGS**. We need to pass all tags into view. However, since the view takes `AddBlogPostRequest.cs` as the `@model`, we need to
	1. Get all tags
	2. Create new `AddBlogPostRequest` instance, `model`
	3. Pass tags into `model`
			```Tags = tags.Select(x => new SelectListItem{ Text=x.Name, Value=x.Id.ToString() }
	4. Pass `model` into view
67. Create tag selection input `select` in `Add.cshtml`
	1. `asp-item`: Use all tags to be options
	2. `asp-for`: Data binding in POST request
68. Create Add action method (POST)
##### Handling Multiple Tags Per Post
69. Modify `SelectedTag` in `AddBlogPostRequest.cs` to `list` with initial value of `Array.Empty<string>()`
##### Saving BlogPost Entity With Tags To The Database
70. Create interface`IBlogPostRepository.cs` in `Repositories`
71. Create CRUD abstract methods
72. Create class `BlogPostRepository.cs`(implements `IBlogPostRepository`) in `Repositories`
73. Create constructor which takes `BlogDbContext` as param
74. Implements `AddAsync`
75. Inject `IBlogPostRepository` into `Program.cs`
76. Add param `IBlogPostRepository` into constructor of `AdminBlogPostsController.cs`
77. Map view model instance (`addBlogPostRequest`) to domain model instance (`blogPost`)
	1. Map all props except `Tags`
	2. Create temporary list var `selectedTags` (not from view model)
	3. Loop through all tags from `addBlogPostRequest.SelectedTags`
		1. Find the tag through `blogDbContext`
		2. Add to temporary list `selectedTags`
	4. Set `blogPost.Tags = selectedTags`
78. Add to database and save
##### Display All Blogs (Get All Blogs)
79. Create List action method (GET)
80. Create view `List.cshtml` and header
81. Implements `GetAllAsync` (use `Include(x=>x.Tags)` for the related prop)
82. Get all blog posts in List action method and pass them to view
83. Declare `@model` for `List.cshtml`
84. Conditional render table to display blog posts
	1. `Id`
	2. `Heading`
	3. `Tags`
85. Add dropdown option to navigate List action method
86. Redirect to List action after adding a new blog post
##### Display Edit Blog Functionality
87. Add `Edit` table column in `List.cshtml`(using `asp` attributes to navigate)
88. Create Edit action method (GET)
89. Create view `Edit.cshtml` and header
90. Implements `GetAsync`
91. Get `blogPost` by `Id`
92. Create view model `EditBlogPostRequest.cs` in `Models > View`
93. Map domain model instance (`blogPost`) to view model instance (`EditBlogPostRequest`) and pass the view model instance to view. Setup of `model.Tags` and `model.SelectedTags`:
	1. `model.Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });`
	2. `model.SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray();`
94. Declare `@model` in `Edit.cshtml`
95. Create `form` in `Edit.cshtml` (conditional render if `Model` is not empty)

##### Edit or Update Blog Functionality
96. Create update button in `Edit.cshtml`
97. Create Edit action method (POST)
98. Map view model to domain model
	1. Map all props except `Tags`
	2. Create temporary list `selectedTags`
	3. Loop through all selected tags in `editBlogPostRequest`
	4. Use `Guid.TryParse` to get tag `Id` 
	5. Get `selectedTag` by `Id`
	6. Add to `selectedTags`
	7. Map `selectedTags` to `blogPost.Tags`
99. Implements `UpdateAsync`
100. Run `UpdateAsync` in Edit action method (POST)
##### Delete Blog Functionality
101. Create delete button in `Edit.cshtml`
102. Create Delete action method (POST)
103. Implements `DeleteAsync`
104. Complete the Delete action method
### Adding WYSIWYG Editor and Image Upload in ASP.NET Core MVC
##### Add WYSIWYG Editor on Add and Edit BlogPost Page
105. Go to Froala > Get Started > USE CDN
106. Copy CDN and paste to `_Layout.cshtml`
107. Apply Froala `Content` input in `Add.cshtml` and `Edit.cshtml`
	- Add the below block at the end of the `.cshtml`
```
@section Scripts {
    <script>
        var editor = new FroalaEditor('#content');
    </script>
}
```
##### Create Image API Controller, POST Method and Image Repository
108. Create `ImagesController` (API controller) in `Controllers`
109. Create `UploadAsync` action method (POST) taking `IFormFile file` as input
110. Create interface`IImageRepository.cs` in `Repositories`
111. Create abstract method `UploadAsync` (return URL string) in the interface
112. Create class `CloudinaryImageRepository.cs` implemented from `IImageRepository`
##### Setup Cloudinary and UploadAsync in Repo
113. Run `Install-Package CloudinaryDotNet` in PM console
114. Setup work env in `appsettings.json`
```
"Cloudinary": {
  "CloudName": "dowhwq18m",
  "ApiKey": "853182862485585",
  "ApiSecret": "**********************"
}
```
115. Create constructor in `CloudinaryImageRepository.cs` taking `IConfiguration` and create `Account` for Cloudinary
	1. Follow instruction in Cloudinary Getting Started page
	2. Use `configuration.GetSection("Cloudinary")` to get value
116. Copy below and paste in `UploadAsync` repo method (upload to cloudinary)
```
var uploadParams = new ImageUploadParams()
{
    File = new FileDescription(file.FileName, file.OpenReadStream()),
    DisplayName = file.FileName,
};
var uploadResult = await cloudinary.UploadAsync(uploadParams);
```
117. `UploadAsync` returns `uploadResult.SecureUri.ToString()`if 
	- `uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK`
##### Inject Image Repository and Test
118. Inject `IImageRepository` in `Program.cs`
119. Create constructor in `ImagesController.cs` taking `IImageRepository`
120. Upload file using `imageRepo.UploadAsync`
121. Return
	1. `Problem(message, null, (int)HttpStatusCode.InternalServerError)` (if URL is not returned by `UploadAsync`)
	2. `Json( new { link = URL } )` (if URL is returned)
122. Test with Postman

##### Call Image Upload From View
123. Create an input type `file` above Featured Image URL such that user can upload file in `Add.cshtml`
124. Add change event listener to `featuredImageUpload` 
125. Create function for the listener to trigger
126. Instantiate a new `FormData` and append the incoming file `e.target.files[0]`
127. Fetch data (POST) to the controller using with `headers: { 'Accept': '*/*' }`
	- Convert response to JSON and console log it
128. Get HTML element `#featuredImageUrl`
129. Auto-fill `#featuredImageUrl` with image URL (switch off `readonly` when changing value)
130. Add `<img />` below Feature Image Upload to display the image after the upload, below shows initial setup
	1. `<img src="" id=featuredImageDisplay style="display: none; width: 300px;" />`
131. Get HTML element `#featuredImageDisplay` and switch on after uploading the image
##### Add FeaturedImageUpload To Edit Page
132. Copy from `Add.cshtml` to `Edit.cshtml`
	- Display image straight-away if URL exists
##### Add Image Upload To Froala WYSIWYG Editor
133. Add `imageUploadURL: '/api/images'` to tell Froala Editor to use controller to upload image instead of local host
### Displaying Blogs and Tags To Users
##### Seeding Blog and Tag Data
134. Run `BlogDataSeed.sql`
##### Displaying Blogs and Hero Section
135. Modify `Index.cshtml` for home controller (should include header and an intro paragraph)
136. Use Pexels to find a good pic for the home page
137. Add `blogPostRepo` input to the constructor of `HomeController.cs`
138. Get all posts in Index action method and pass to view
139. Declare `@model` for data binding
140. Display all blog post in `Index.cshtml` which includes
	1. Image
	2. Heading
	3. Author
	4. Publish date
	5. Tags
	6. Short description
	7. Show more button

##### Display Single Blog Post and Blog Details
141. Create `BlogsController.cs`
142. `Index` should takes `string urlHandle`
143. Add `asp` navigation to `Read More` button in `Index.cshtml`
144. Add `GetByUrlHandleAsync` in `IBlogPostRepo`
145. Implements `GetByUrlHandleAsync` in `BlogRepo`
146. Create constructor in `BlogsController.cs` taking `IBlogPostRepo`
147. In Index action method, get blog post by urlHandle and pass it to view
148. Create view `Index.cshtml` with data model binding. Page includes
	1. Heading
	2. Author
	3. Published Date
	4. Tags
	5. Image
	6. Content (use `@Html.Raw` to display)
149. Change the page title by overriding `ViewData["Title"]` from `Index.cshtml` to `_Layout.cshtml`
##### Displaying Tags On Home Page
150. Add `ITagRepo` to the constructor of the `HomeController.cs`
151. Get all tags in index action method
152. Create `HomeViewModel.cs` in `Models > View` to combine the two view models, `BlogPost` and `Tag`
153. Create `IEnumerable` props for `BlogPosts` and `Tags`
154. Modify `@model` in `Index.cshtml` to `HomeViewModel`
155. Change the original `Model` to `Model.BlogPosts`
156. Create `<div>` to show tags 
157. Instantiate `HomeViewModel` in Index action method and pass to view
### Authentication and Authorization in ASP.NET MVC (ASP.NET Core Identity)
##### Install Identity Packages and Config All Roles and SuperAdmin Using `AspNetCore.Identity`
158. Install `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
159. Create `AuthDbContext.cs` (Inherited from `IdentityDbContext`) in `Data`
160. Create constructor
161. Override `OnModelCreating` with `protected` access modifier
162. Create `List<IdentityRole>` for `Admin`, `SuperAdmin` and `User` (`IdentityRole`includes `Name`, `NormalizedName`, `Id` and `ConcurrencyStamp`)
163. Build entity `IdentityRole` with initial data `roles`
`builder.Entity<IdentityRole>().HasData(roles)`
164. Create `IdentityUser` for `superAdminUser` (`IdentityUser` includes `UserName`, `Email`, `NormalizedEmail`, `NormalizedUserName`, `Id`)
165. Create hashed password using `PasswordHasher` for `IdentityUser` and assign it to `superAdminUser.PasswordHash`
`superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
`                        .HashPassword(superAdminUser, "AdminPass@123");`
166. Build entity `IdentityUser` with initial data `superAdminUser`
167. Create `List<IdentityUserRole<string>>` for `superAdminRoles` and add all roles (`IdentityUserRole` includes `RoleId` and `UserId`)
168. Build entity `IdentityUserRole` with initial data `superAdminRoles`

##### Add ConnectionString and Inject AuthDbContext in Program
169. Add `ConnectionStrings` in `appsettings.json`
170. Inject `AuthDbContext`in `Program.cs
171. Use `IdentityUser` and `IdentityRole` as data storage in `AuthDbContext`
172. Add `UseAuthentication` to the app
##### Run Entity Framework Core Migrations
173. Run `Add-Migration "name" -Context "AuthDbContext"` and `Update-Database -Context "AuthDbContext"`
##### Register User Functionality
174. Create `AccountController.cs`
175. Create Register action method (GET)
176. Create view `Register.cshtml` with a form includes `Username`, `Email` and `Password` for registration
177. Create Register action method (POST)
178. Create `RegisterViewModel`
179. Declare `@model` in `Register.cshtml`
180. Data binding with `asp-for`
181. Create constructor for `AccountController.cs` which takes `UserManager<IdentityUser>` as param
182. Instantiate domain model `IdentityUser` in Register (POST) with UserName and Email (no password)
183. Use `userManager.CreateAsync` to create `identityUser` (2nd arg is the password) and return the creation response `createUserResponse`
184. If account creation is successful, using `userManager.AddToRoleAsync` to add `identityUser` to a specific role, eg. `User` and redirect to `Register` if successful
185. Specify the generic type for `DbContextOptions` in both `BlogDbContext.cs` and `AuthDbContext.cs`
186. Create Register button in `_Layout.cshtml`
187. Create default setting for password requirement in `Program.cs`

##### Login User Functionality
188. Create Login action method (GET)
189. Create view `Login.cshtml`
190. Use the form from `Register.cshtml` to `Login.cshtml` (with modification)
191. Create view model `LoginViewModel.cs` and props
192. Declare `@model`
193. Create Login action method (POST)
194. Add `SignInManager` to the constructor
195. Use `signInManager.PasswordSignInAsync` to login
196. Create the Login button in `_Layout.cshtml`
197. Inject `SignInManager` in `_Layout.cshtml`
```
@using Microsoft.AspNetCore.Identity
@inject SignInManager<IdentityUser> signInManager
```
198. If `signInManager.IsSignedIn(USer)`,  display username and logout button at the right corner of the nav-bar

##### Logout User Functionality
199. Create Logout action method (GET)
200. Use `signInManager.SignOutAsync` to logout
##### Authorization in our Application
201. Add decorate attribute `[Authorize]` to Add (GET) in `AdminTagsController` (require logged in to navigate to this page)
##### Adding Role Based Authorization
202. Add `Roles = "Admin"` to `Authorize` to restrict access from other users
203. Create AccessDenied action method (GET)
204. Create view `AccessDenied.cshtml` with basic warning message
205. Add condition to render admin dropdown list (`signInManager.IsSignedIn(User)` and `User.IsInRole("Admin")`)
206. Copy authorize decorator to all admin action methods / put at the top of the controller
##### Adding ReturnUrl After Authorization
207. Add `ReturnUrl` as a string param of Login action method
208. Create new prop `ReturnUrl` to `LoginViewModel`
209. Instantiate `LoginViewModel` with `ReturnUrl` and pass to view
210. Create `input` of `hidden` type with `asp-for="ReturnUrl"`
211. After successful logged in, if `!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl)`, redirect to `returnUrl`
### Add Like and Comment Functionality In Our Blog
##### Domain Models For Like Functionality and Migrations
212. Create domain model `BlogPostLike.cs` and props (`Id`, `PostId`, `UserId`)
213. Add `ICollection<BlogPostLike>` in `BlogPost.cs` to specify related prop
214. Add `DbSet` prop in `BlogDbContext` for `BlogPostLike`
215. Run migration and update database in PM console
##### Display Total Likes (Likes Repository)
216. Create `<div>` to show likes below Author displayed in Blogs Index View (set Default value first)
217. Create interface `IBlogPostLikeRepository.cs`
218. Create abstract method `GetTotalLikesAsync` which takes `blogPostId`
219. Create repository `BlogPostLikeRepository.cs` (implements interface)
220. Create constructor for repo and pass `BlogDbContext`
221. In `GetTotalLikesAsync`, use `CountAsync` from `blogDbContext` to count the like by post Id
222. Inject `IBlogPostLikeRepository.cs` into `Program.cs`
223. Add param `IBlogPostLikeRepository` to constructor in `BlogsController.cs`
224. In Index action method, get total likes if `blogPost` exists
225. Create new view model `BlogDetailsViewModel` to combine two domain models `BlogPost` and `BlogPostLike` (all props from `BlogPost` + `TotalLikes`)
226. Map all props from domain model (`blogPost`) to view model (`blogDetailsViewModel`) and pass to view
227. Change `@model` in `Index.cshtml` (Blogs) from `BlogPost` to `BlogDetailsViewModel` and modify the `<div>` in step 216 to display likes
##### Implement Like Functionality
228. Copy and paste CDN for bootstrap icons to `_Layout.cshtml`
229. Inject `SignInManager` and `UserManager` to `Index.cshtml` (Blogs)
230. Show clickable thumbs up icon (search in bootstrap) if `signInManager.IsSignedIn(User)`
	- `id="btnLike"`
	- `style="cursor: pointer"`
231. Create API controller `BlogPostLikeController`
232. Create AddLike action method (POST) with `Route("Add")`
233. Create view model `AddLikeRequest` including props `BlogPostId` and `UserId`
234.  AddLike action should takes `AddLikeRequest` (bound from request body `[FromBody]`) as param
235. Add abstract method `AddLikeForBlog` (param: `BlogPostLike`) to `IBlogPostLikeRepo`
236. Implement `AddLikeForBlog` in `BlogPostLikeRepo` (using `AddAsync` and `SaveChangesAsync`)
237. Create constructor for `BlogPostLikeController` which takes `IBlogPostLikeRepo` as param
238. Map from view model (`addLikeRequest`) to domain model (`blogPostLike`)
239. Run `AddLikeForBlog` to database
240. Return successful response `Ok()`
241. Add event listener to `#btnLike` for `click` event which triggers `addLikeForBlog`
242. Create function `addLikeForBlog` which fetches the Add like endpoint with post method
```
async function addLikeForBlog() {
    fetch('/api/BlogPostLike/Add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*'
        },
        body: JSON.stringify({ 
            blogPostId: '@Model.Id',
            userId: '@userManager.GetUserId(User)'
        })
    })
}
```
##### Updates To Like Functionality
243. Search thumbs up fill icon from bootstrap icons
244. Modify the `.then` in `addLikeForBlog` such that 
	1. `btnLikeElement.innerHTML` change to thumbs up fill icon
	2. Remove event listener to disallow clickable
	3. Run `getTotalLikes()` to refresh the number of like (function will be created later)
245. Create GetTotalLikesForBlog action method (GET)
	1. Use path variable as route `[Route("{blogPostId:Guid}/total_likes")]`
	2. Use `blogPostId` as param of action method `[FromRoute] Guid blogPostId` 
	3. Get total likes using `blogPostLikeRepo`
	4. Return `Ok(totalLikes)`
246. Add `id="totalLike"` to the total like display for JS
247. Create function `getTotalLikes` in JS section in `Index.cshtml`
248. Fetch GET request to get total likes and pass to `totalLikes.innerHTML`
249. Add `SignInManager`, `UserManager` in `BlogsController`
250. Create abstract method `GetLikesForBlog` in `IBlogPostLikeRepo` to retrieve all likes for a specific post
251. Implement `GetLikesForBlog` in `BlogPostRepo` using `blogDbContext.BlogPostLikes.Where(x => x.PostId == blogPostId).ToListAsync()`
252. Create new var `liked=false` to indicate initialize the state of the post liked by the logged in user
253. Get Likes for the blog `likesForBlog` if user is logged in
254. Get `userId` using `userManager`
255. Check if `likesForBlog` contains the current user Id, if so, set `liked` to true
256. Add `liked` into view model `blogDetailsViewModel`
257. Add prop `liked` to view model entity
258. In `Index.cshtml`, modify:
```
If user is logged in:
	If user liked the post:
		Display the like button without id and use thumbs-up-fill
	else:
		Display the original one
```
##### Domain Models For Comment Functionality and Migration
259. Create domain model for `BlogPostComment` with props
	- `Id`, `Description`, `BlogPostId`, `UserId`, `DateAdded`
260. Add related props to `BlogPost` domain model
261. Add `DbSet` to `BlogDbContext` for `BlogPostComments`
262. Add migration and update database
##### Add Comment Functionality
263. Create a `div` with `class="card"`after the post content. Inside it, 
	1. Create another `div` with `class="card-header"` to wrap `form` (`input` and `button`) to post comments if the user is logged in
	2. Create  a `div` with `class="card-body"` to display all comments about the post
264. Create Index action method (POST) in `BlogsController`
265. Add new prop `CommentDescription` to handle add comment request
266. Add hidden input after submit button to bind with the `Id` of the view model
267. Create `IBlogPostCommentRepository` with abstract method
	1. `AddAsync()`
268. Create `BlogPostCommentRepository`, create constructor and implement interface method
269. Inject new Repo to `Program.cs`
270. Add `BlogPostCommentRepo` to the constructor of `BlogsController.cs`
271. In Index action method (POST), map view model (`blogDetailsViewModel`) to domain model (`blogPostComment`), followed by adding comments to database (if user is logged in)
	- For `DateAdded` prop, use `DateTime.Now`
	- `return RedirectToAction` after save to database
	- `return View()` if user is not logged in
272. Create another hidden input to store and pass `urlHandle` for the redirection after adding comments
##### Display Comments
273. Create a new view model `BlogComment`(store all details per comment and pass to `index` view to display) which includes
	1. `string Description`
	2. `DateTime DateAdded`
	3. `string Username`
274. Add method `GetCommentByBlogIdAsync` to comment repo (include interface)
275. Get all comments by post Id (before mapping) in Index action method (GET)
276. Add another prop `IEnumerable<BlogComment>` to `BlogDetailsViewModel` to store list of comments
277. Create `blogCommentsForView` and loop through `blogComments` to add comment one by one:
```
blogCommentsForView.Add(new BlogComment
{
    Description = blogComment.Description,
    DateAdded = blogComment.DateAdded,
    Username = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
});
```
278. Add `blogCommentsForView` into the view model
279. Below add comment form, If comments exists, loop through the comment list and display it
```
@if (Model.Comments != null && Model.Comments.Any())
{
    foreach (var comment in Model.Comments)
    {
        <div class="card mb-3">
            <div class="card-body">
                @comment.Description
            </div>
            <div class="d-flex justify-content-between">
                <span class="text-secondary">@comment.Username</span>
                <span class="text-secondary">@comment.DateAdded.ToShortDateString()</span>
            </div>
        </div>
    }
}
```
### Manage Users - Create Read Delete
##### Display Users
280. Create `AdminUsersController` for admin to manage users
281. Create List action method (GET)
282. Create interface `IUserRepo` and create abstract methods
	1. `GetAll()`
283. Create class `UserRepo` with constructor taking `AuthDbContext` to retrieve user from database
284. Implement `GetAll()` in `UserRepo`
	1. Get all users using `authDbContext.Users.ToListAsync()`
	2. Get `superAdminUser` by Email
	3. Remove `superAdminUser` from users (check if `superAdminUser != null`)
	4. return users
285. Inject `UserRepo` into `Program.cs`
286. Create constructor in `AdminUsersController` taking `IUserRepo`
287. Create two new view models:
	1. `User`: to store user information (`Id`, `Username`, `EmailAddress`)
	2. `UserViewModel`: to store a list of users
288. Implement List action method (GET)
	1. Get all users using `userRepo`
	2. Instantiate new `usersViewModel` and empty `usersViewModel.users`
	3. Loop through all users and add to `usersViewModel.users` one by one
	4. Pass the view model to view
289. Create new view for `List` action method with header and @model
290. In `List.cshtml`, add table to display all users (`Id`, `Username`, `EmailAddress`)
291. Add authorization to `AdminUsersController` using `[Authorize(Roles= "Admin")]`
292. Add another dropdown option to `_layout.cshtml` to display all user for admin
##### Create New User
293. Create button in `List.cshtml` to create new user (copy from bootstrap)
294. Add modal at the end of `List.cshtml` (copy from bootstrap)
295. Inside modal body, we have 5 inputs:
	1. `Username`: text
	2. `Email Address`: text
	3. `Password`: password
	4. `User Role`: checkbox
	5. `Admin role`: checkbox
296. Change `Save changes` button to `submit` type
297. Add `Username`, `Email`, `Password`, `AdminRoleCheckbox` props to the view model to receive input
298. Wrap the entire modal by form tags in `List.cshtml` and perform data binding using `asp-for`
299. Create List action method (POST)
300. Inject `UserManager` to `AdminUserController` constructor
301. Implement List action method (POST)
	1. Create new domain model instance `identityUser`
	2. Create new user using `userManager.CreateAsync`
	3. If creation succeeded, create a list of string called `roles` with one item `User`
	4. If admin checkbox is selected, add `Admin` to `roles`
	5. Assign roles to `identityUser` using `userManager.AddToRolesAsync`
	6. If succeeded, redirect to `AdminUsers.List`, otherwise `View`
302. Wrap the admin checkbox with a condition `if (User.IsInRole("SuperAdmin"))`
##### Delete User
303. Create another column in `List.cshtml` as a form to delete user
```
<form method="post" 
	asp-controller="AdminUsers" 
	asp-action="Delete" 
	asp-route-id="@user.Id">
	<button class="btn btn-danger" type="submit">Delete</button>
</form>
```
304. Create and implement Delete action method (POST)
	1. Get user by `id` using `userManager.FindByIdAsync`
	2. If user is not null, delete user
### Validations - Server Side and Client Side Validations
##### Register Page - Server Side Validations
305. In `AccountController Register (POST)`, there is no validation on `RegisterViewModel`. Therefore, we need to add validation (`[Required]`) to `RegisterViewModel` props.
306. In `AccountController Register (POST)`, wrap the entire body (except return) within a condition `if (ModelState.IsValid)`
307. In `Register.cshtml`, add `<span class="text-danger" asp-validation-for="prop name">` below each input to display error message if necessary
308. Add `[EmailAddress]` annotation above Email in `RegisterViewModel` to validate input format.
309. Add `[MinLength(8, ErrorMessage = "Password has to be at least 8 characters" )]` annotation above Password in `RegisterViewModel` to validate password length with customized error message.
##### Register Page - Client Side Validations
310. Add `required` attribute to `input` in `Register.cshtml`
311. Add `minlength="8"` attribute to password input
##### Login Page Validations
312. Add annotations to input
313. Check `ModelState.IsValid` in Login action method
314. Add `required` and `minlength` attributes to input in `Login.cshtml`
315. Add `span` in `Login.cshtml`
##### Add New Tag Page - Validations
316. Add `[Required]` to view model `AddTagRequest`
317. Check `ModelState`
318. Add `required` attribute and `span`
##### Custom Validations
319. Add `ValidateAddTagRequest(addTagRequest)` at the beginning of the Add action method (POST)
320. Implement `ValidateAddTagRequest` by checking if `Name == DisplayName`, add customized error
`ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName")`
