create database KnowledgeSharing
use KnowledgeSharing

create table Categories(
CategoryID int primary key identity(1,1),
CategoryName nvarchar(max))
go

create table Users(
UserID int primary key identity(1,1),
Email nvarchar(max),
PasswordHash nvarchar(max),
Name nvarchar(max),
Mobile nvarchar(max),
IsAdmin bit default(0))
go


create table Questions(
QuestionID int primary key identity(1,1),
QuestionName nvarchar(max),
QuestionDateAndTime datetime,
UserID int references Users(UserID) on delete cascade,
CategoryID int references Categories(CategoryID) on delete cascade,
VotesCount int,
AnswersCount int,
ViewsCount int)
go

create table Answers(
AnswerID int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserID int references Users(UserID),
QuestionID int references Questions(QuestionID) on delete cascade,
VotesCount int)
go

create table Votes(
VoteID int primary key identity(1,1),
UserID int references Users(UserID),
AnswerID int references Answers(AnswerID) on delete cascade,
VoteValue int)
go

select * from Answers
select * from Categories
select * from Questions
select * from Users
select * from Votes



insert into Users values('admin@gmail.com', 'e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7',
'Admin', '9876543210', 1)

-- password:  Admin@123

insert into Users values('user@gmail.com', '3e7c19576488862816f13b512cacf3e4ba97dd97243ea0bd6a2ad1642d86ba72',
'User', '9638527410', 0)
-- password:  User@123

insert into Categories 
values('CSharp')
insert into Categories 
values('Ado.Net')
insert into Categories 
values('MSSQL')
insert into Categories 
values('HTML')


insert into Questions (QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount)
values('How to parse string to double in c#?','2019-03-05 10:03 am',2,1,0,0,0)
insert into Questions (QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount)
values('How to establish connection string in Ado.net?','2020-07-04 06:13 pm',2,2,0,0,0)
insert into Questions (QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount)
values('What is the query for left join?','2019-06-10 12:45 pm',2,3,0,0,0)
insert into Questions (QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount)
values('What is header tag in Html?','2018-12-10 11:50 am',2,4,0,0,0)
