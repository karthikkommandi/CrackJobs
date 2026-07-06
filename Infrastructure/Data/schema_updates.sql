/* ============================================================================
   CrackJob — schema updates + demo seed data
   Adds JobRole, QuestionJobRoleMap and Like tables, extends Comments, and
   seeds a rich demo dataset so the learner-facing app works out of the box.
   Safe to re-run: schema changes are guarded; content tables are reseeded.
   ============================================================================ */
USE CrackJob;
GO

/* ---------- 1. New tables ---------- */
IF OBJECT_ID('dbo.JobRole', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.JobRole (
        RoleID          INT IDENTITY(1,1) PRIMARY KEY,
        RoleName        VARCHAR(200)  NOT NULL,
        RoleDescription VARCHAR(1000) NULL,
        CreatedDate     DATETIME NULL,
        CreatedBy       VARCHAR(50) NULL,
        UpdatedDate     DATETIME NULL,
        UpdatedBy       VARCHAR(50) NULL
    );
END
GO

IF OBJECT_ID('dbo.QuestionJobRoleMap', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.QuestionJobRoleMap (
        ID          BIGINT IDENTITY(1,1) PRIMARY KEY,
        QID         BIGINT NOT NULL,
        RoleID      INT    NOT NULL,
        IsActive    BIT    NOT NULL CONSTRAINT DF_QJRM_IsActive DEFAULT(1),
        CreatedDate DATETIME NULL,
        CreatedBy   VARCHAR(50) NULL,
        UpdatedDate DATETIME NULL,
        UpdatedBy   VARCHAR(50) NULL
    );
END
GO

IF OBJECT_ID('dbo.[Like]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.[Like] (
        ID          BIGINT IDENTITY(1,1) PRIMARY KEY,
        UserID      VARCHAR(50)  NOT NULL,
        TargetType  VARCHAR(20)  NOT NULL,   -- 'question' | 'answer' | 'comment'
        TargetID    BIGINT       NOT NULL,
        CreatedDate DATETIME NULL,
        CONSTRAINT UQ_Like UNIQUE (UserID, TargetType, TargetID)
    );
END
GO

/* ---------- 2. Extend Comments ---------- */
IF COL_LENGTH('dbo.Comments', 'QuestionID') IS NULL
    ALTER TABLE dbo.Comments ADD QuestionID BIGINT NULL;
GO
IF COL_LENGTH('dbo.Comments', 'CreatedDate') IS NULL
    ALTER TABLE dbo.Comments ADD CreatedDate DATETIME NULL;
GO

/* ---------- 3. Reseed content (demo data) ---------- */
DELETE FROM dbo.[Like];
DELETE FROM dbo.QuestionJobRoleMap;
DELETE FROM dbo.QuestionTechnologyMap;
DELETE FROM dbo.QuestionCompanyMap;
DELETE FROM dbo.QuestionAnswerMap;
DELETE FROM dbo.Comments;
DELETE FROM dbo.Answers;
DELETE FROM dbo.Questinons;
DELETE FROM dbo.Technology;
DELETE FROM dbo.Company;
DELETE FROM dbo.JobRole;
DELETE FROM dbo.Rating;
GO

/* Ratings */
INSERT INTO dbo.Rating (RID, StarCount, Description) VALUES
 (1,1,'Poor'),(2,2,'Fair'),(3,3,'Good'),(4,4,'Very Good'),(5,5,'Excellent');
GO

/* Companies */
SET IDENTITY_INSERT dbo.Company ON;
INSERT INTO dbo.Company (ID, CompanyName, CompanyDescription, CreatedDate, CreatedBy) VALUES
 (1,'Google','Search, cloud and AI. Known for deep algorithmic and system-design interviews.', GETUTCDATE(),'seed'),
 (2,'Amazon','E-commerce and AWS. Heavy on Leadership Principles and scalable-system questions.', GETUTCDATE(),'seed'),
 (3,'Microsoft','Windows, Azure and enterprise software. Balanced coding + design rounds.', GETUTCDATE(),'seed'),
 (4,'Meta','Social platforms. Fast-paced coding and product-architecture interviews.', GETUTCDATE(),'seed'),
 (5,'Netflix','Streaming at scale. Senior-heavy, culture and system-design focused.', GETUTCDATE(),'seed');
SET IDENTITY_INSERT dbo.Company OFF;
GO

/* Technologies */
SET IDENTITY_INSERT dbo.Technology ON;
INSERT INTO dbo.Technology (TechID, TechName, TechDescription, CreatedDate, CreatedBy) VALUES
 (1,'C#','.NET language for backend & enterprise apps.', GETUTCDATE(),'seed'),
 (2,'React','Component-based JavaScript UI library.', GETUTCDATE(),'seed'),
 (3,'SQL','Relational databases and query optimization.', GETUTCDATE(),'seed'),
 (4,'System Design','Designing scalable, reliable distributed systems.', GETUTCDATE(),'seed'),
 (5,'Data Structures & Algorithms','Core problem-solving foundations.', GETUTCDATE(),'seed'),
 (6,'JavaScript','The language of the web.', GETUTCDATE(),'seed');
SET IDENTITY_INSERT dbo.Technology OFF;
GO

/* Job Roles */
SET IDENTITY_INSERT dbo.JobRole ON;
INSERT INTO dbo.JobRole (RoleID, RoleName, RoleDescription, CreatedDate, CreatedBy) VALUES
 (1,'Frontend Developer','Builds user interfaces with modern web frameworks.', GETUTCDATE(),'seed'),
 (2,'Backend Developer','Designs APIs, services and databases.', GETUTCDATE(),'seed'),
 (3,'Full Stack Developer','Works across the entire web stack.', GETUTCDATE(),'seed'),
 (4,'Data Engineer','Builds data pipelines and warehousing solutions.', GETUTCDATE(),'seed'),
 (5,'Engineering Manager','Leads engineering teams and technical delivery.', GETUTCDATE(),'seed');
SET IDENTITY_INSERT dbo.JobRole OFF;
GO

/* Questions */
SET IDENTITY_INSERT dbo.Questinons ON;
INSERT INTO dbo.Questinons (QID, QName, QDesc, CreatedDate, CreatedBy) VALUES
 (1,'Explain the difference between value types and reference types in C#.','Cover stack vs heap allocation, boxing/unboxing and common pitfalls.', GETUTCDATE(),'seed'),
 (2,'How does React''s reconciliation (virtual DOM diffing) work?','Explain keys, the diffing heuristic and how it improves performance.', GETUTCDATE(),'seed'),
 (3,'Design a URL shortener like bit.ly.','Discuss encoding, storage, scale, caching and redirection.', GETUTCDATE(),'seed'),
 (4,'What is the difference between INNER JOIN and LEFT JOIN in SQL?','Include examples and when each is appropriate.', GETUTCDATE(),'seed'),
 (5,'Reverse a linked list, iteratively and recursively.','Discuss time/space complexity of each approach.', GETUTCDATE(),'seed'),
 (6,'Explain useEffect and its dependency array.','When does it run, cleanup functions, and common mistakes.', GETUTCDATE(),'seed'),
 (7,'Design a scalable news feed system.','Fan-out on write vs read, ranking, caching and pagination.', GETUTCDATE(),'seed'),
 (8,'What are async/await and how do they work under the hood in C#?','State machines, Task, ConfigureAwait and deadlocks.', GETUTCDATE(),'seed');
SET IDENTITY_INSERT dbo.Questinons OFF;
GO

/* Answers */
SET IDENTITY_INSERT dbo.Answers ON;
INSERT INTO dbo.Answers (AID, Answer, AnswerReference, CreatedDate, CreatedBy) VALUES
 (1,'Value types (structs, int, bool) hold their data directly and are typically allocated on the stack; reference types (classes, arrays, strings) hold a reference to data on the heap. Assigning a value type copies the value, while assigning a reference type copies the reference. Boxing converts a value type to object (heap), unboxing reverses it — both have a performance cost.','https://learn.microsoft.com/dotnet/csharp/', GETUTCDATE(),'seed'),
 (2,'React builds a virtual DOM tree on each render and diffs it against the previous tree. It uses a heuristic O(n) algorithm: elements of different types are replaced wholesale, and lists rely on stable "key" props to match children between renders. Only the minimal set of real DOM mutations are applied.','https://react.dev/learn/preserving-and-resetting-state', GETUTCDATE(),'seed'),
 (3,'Generate a short unique key (base62 of an auto-increment id or a hash), store key->longUrl in a fast key-value store, and 301/302 redirect on lookup. Scale reads with caching (e.g. Redis) and a CDN; partition the datastore by key range or hash. Handle custom aliases, expiration and analytics separately.','https://en.wikipedia.org/wiki/URL_shortening', GETUTCDATE(),'seed'),
 (4,'INNER JOIN returns only rows with matches in both tables. LEFT JOIN returns all rows from the left table plus matched rows from the right, with NULLs where there is no match. Use LEFT JOIN when you need to keep unmatched left-side rows (e.g. customers with no orders).','https://learn.microsoft.com/sql/', GETUTCDATE(),'seed'),
 (5,'Iterative: walk the list keeping prev/curr pointers, reversing next at each step — O(n) time, O(1) space. Recursive: reverse the rest of the list then fix the current node''s link — O(n) time, O(n) stack space. The iterative version is preferred for large lists to avoid stack overflow.','https://en.wikipedia.org/wiki/Linked_list', GETUTCDATE(),'seed'),
 (6,'useEffect runs after render. With no dependency array it runs after every render; with [] it runs once on mount; with [a,b] it runs whenever a or b change. Return a cleanup function to undo subscriptions/timers. Common mistakes: missing dependencies (stale closures) and running expensive work every render.','https://react.dev/reference/react/useEffect', GETUTCDATE(),'seed'),
 (7,'Model posts, follows and a feed. Fan-out-on-write pushes new posts into followers'' feeds (fast reads, expensive for celebrities); fan-out-on-read assembles the feed at query time (cheap writes, heavier reads). Hybrid approaches handle high-fan-out users. Cache hot feeds, rank by relevance, and paginate with cursors.','https://en.wikipedia.org/wiki/News_feed', GETUTCDATE(),'seed'),
 (8,'async/await lets you write asynchronous code sequentially. The compiler rewrites an async method into a state machine that yields at each await and resumes on completion of the awaited Task. ConfigureAwait(false) avoids capturing the synchronization context; blocking on async code (.Result/.Wait) can deadlock in UI/ASP.NET contexts.','https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/', GETUTCDATE(),'seed');
SET IDENTITY_INSERT dbo.Answers OFF;
GO

/* Question <-> Answer maps (Id is not identity in this table) */
INSERT INTO dbo.QuestionAnswerMap (ID, QID, AID, IsActive, RatingId, CommentId, CreatedBy, CreatedDate) VALUES
 (1,1,1,1,5,0,'seed',GETUTCDATE()),
 (2,2,2,1,5,0,'seed',GETUTCDATE()),
 (3,3,3,1,4,0,'seed',GETUTCDATE()),
 (4,4,4,1,5,0,'seed',GETUTCDATE()),
 (5,5,5,1,4,0,'seed',GETUTCDATE()),
 (6,6,6,1,5,0,'seed',GETUTCDATE()),
 (7,7,7,1,4,0,'seed',GETUTCDATE()),
 (8,8,8,1,5,0,'seed',GETUTCDATE());
GO

/* Question <-> Company maps */
INSERT INTO dbo.QuestionCompanyMap (ID, QID, CompID, IsActive, CreatedBy, CreatedDate) VALUES
 (1,1,3,1,'seed',GETUTCDATE()),   -- C# @ Microsoft
 (2,1,2,1,'seed',GETUTCDATE()),   -- C# @ Amazon
 (3,2,4,1,'seed',GETUTCDATE()),   -- React @ Meta
 (4,2,1,1,'seed',GETUTCDATE()),   -- React @ Google
 (5,3,1,1,'seed',GETUTCDATE()),   -- URL shortener @ Google
 (6,3,2,1,'seed',GETUTCDATE()),   -- URL shortener @ Amazon
 (7,4,3,1,'seed',GETUTCDATE()),   -- SQL @ Microsoft
 (8,5,1,1,'seed',GETUTCDATE()),   -- Linked list @ Google
 (9,5,4,1,'seed',GETUTCDATE()),   -- Linked list @ Meta
 (10,6,4,1,'seed',GETUTCDATE()),  -- useEffect @ Meta
 (11,7,4,1,'seed',GETUTCDATE()),  -- news feed @ Meta
 (12,7,5,1,'seed',GETUTCDATE()),  -- news feed @ Netflix
 (13,8,3,1,'seed',GETUTCDATE());  -- async @ Microsoft
GO

/* Question <-> Technology maps */
INSERT INTO dbo.QuestionTechnologyMap (ID, QID, TechID, IsActive, CreatedDate) VALUES
 (1,1,1,1,GETUTCDATE()),  -- C#
 (2,2,2,1,GETUTCDATE()),  -- React
 (3,2,6,1,GETUTCDATE()),  -- JavaScript
 (4,3,4,1,GETUTCDATE()),  -- System Design
 (5,4,3,1,GETUTCDATE()),  -- SQL
 (6,5,5,1,GETUTCDATE()),  -- DSA
 (7,6,2,1,GETUTCDATE()),  -- React
 (8,7,4,1,GETUTCDATE()),  -- System Design
 (9,8,1,1,GETUTCDATE());  -- C#
GO

/* Question <-> Job Role maps */
INSERT INTO dbo.QuestionJobRoleMap (QID, RoleID, IsActive, CreatedBy, CreatedDate) VALUES
 (1,2,1,'seed',GETUTCDATE()),  -- C# -> Backend
 (1,3,1,'seed',GETUTCDATE()),  -- C# -> Full Stack
 (2,1,1,'seed',GETUTCDATE()),  -- React -> Frontend
 (2,3,1,'seed',GETUTCDATE()),  -- React -> Full Stack
 (3,2,1,'seed',GETUTCDATE()),  -- URL shortener -> Backend
 (3,5,1,'seed',GETUTCDATE()),  -- URL shortener -> Eng Manager
 (4,2,1,'seed',GETUTCDATE()),  -- SQL -> Backend
 (4,4,1,'seed',GETUTCDATE()),  -- SQL -> Data Engineer
 (5,1,1,'seed',GETUTCDATE()),  -- Linked list -> Frontend
 (5,2,1,'seed',GETUTCDATE()),  -- Linked list -> Backend
 (6,1,1,'seed',GETUTCDATE()),  -- useEffect -> Frontend
 (7,2,1,'seed',GETUTCDATE()),  -- news feed -> Backend
 (7,5,1,'seed',GETUTCDATE()),  -- news feed -> Eng Manager
 (8,2,1,'seed',GETUTCDATE());  -- async -> Backend
GO

/* A couple of seed comments so threads aren't empty */
INSERT INTO dbo.Comments (Comment, UserID, ParrentCommentID, LikeCount, ReplyCount, IsDeleted, QuestionID, CreatedDate) VALUES
 ('Great explanation — the boxing/unboxing note is exactly what tripped me up in my last interview!','priya', NULL, 3, 1, 0, 1, GETUTCDATE());
DECLARE @c1 BIGINT = SCOPE_IDENTITY();
INSERT INTO dbo.Comments (Comment, UserID, ParrentCommentID, LikeCount, ReplyCount, IsDeleted, QuestionID, CreatedDate) VALUES
 ('Agreed. Remember that strings are reference types but behave immutably.','arjun', @c1, 1, 0, 0, 1, GETUTCDATE()),
 ('The stable-key point is so important — I once used array index as key and got weird UI bugs.','sam', NULL, 2, 0, 0, 2, GETUTCDATE());
GO

PRINT 'CrackJob schema + seed complete.';
GO
