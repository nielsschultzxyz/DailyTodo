-- CREATE TABLE AppUser & DailyTodo

CREATE TABLE AppUser 
(
	UserID int IDENTITY(1,1) primary key NOT NULL,
	Firstname nvarchar(100),
	Lastname nvarchar(100),
	Username nvarchar(100) NOT NULL,
	UserPassword nvarchar(50) NOT NULL,
	Email nvarchar(100)
);

CREATE TABLE DailyTodo 
(
	TodoID int IDENTITY(1,1) primary key NOT NULL,
	UserID int foreign key references AppUser(UserID), --foreign key
	Todoname nvarchar(100) NOT NULL,
	TaskDescription nvarchar(500),
	IsTodoFinished int,
	ClosedDate Date,
	CreateDate Date NOT NULL,
	FinishedTillDate Date NOT NULL
);

-- INSERT INTO AppUser TestUser in terms to Test the login

INSERT INTO AppUser (Username, UserPassword) values
 ('TestUser', 'UserPassword'),
 ('Admin', 'Password');

SELECT * FROM AppUser


-- DailyTodo
SELECT * FROM DailyTodo

SELECT * FROM DailyTodo WHERE FinishedTillDate = '2023-03-15'

-- Delete
DELETE FROM DailyTodo WHERE UserID = 1

-- INSERT
INSERT INTO DailyTodo (
	UserID, Todoname, TaskDescription, IsTodoFinished, CreateDate, FinishedTillDate
	)
	VALUES
		(1, 'new Todo', 'es gibt was zutun', 0, '2023-03-12', '2023-03-15'),	
		(1, 'new Todo2', 'es gibt was zutun', 0, '2023-03-12', '2023-03-15'),
		(1, 'new Todo3', 'es gibt was zutun', 0, '2023-03-12', '2023-03-15');


INSERT INTO DailyTodo (TodoID, UserID, Todoname, TaskDescription, IsTodoFinished, ClosedDate, CreateDate, FinishedTillDate) VALUES
	(1, 1, 'new Todo,', 'es gibt was zutun', 0, NULL, '2023-03-07', '2023-03-08')

-- Update 
UPDATE DailyTodo SET FinishedTillDate = 1;

UPDATE DailyTodo SET IsTodoFinished = 1
	WHERE 
		UserID = 1 AND 
		Todoname = 'TestTodoSetAsDone' AND 
		FinishedTillDate = '2023-04-17';