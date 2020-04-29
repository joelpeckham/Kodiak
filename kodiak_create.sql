use kodiakDB

-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-04-27 18:06:53.45

-- tables
-- Table: Activity
CREATE TABLE Activity
(
    activityID int NOT NULL IDENTITY,
    activityTypeID varchar(50) NOT NULL,
    name varchar(50) NOT NULL,
    data varchar(8000) NULL,
    CONSTRAINT Activity_pk PRIMARY KEY  (activityID)
);

-- Table: ActivityType
CREATE TABLE ActivityType
(
    activityTypeID varchar(50) NOT NULL,
    Description varchar(400) NOT NULL,
    CONSTRAINT ActivityType_pk PRIMARY KEY  (activityTypeID)
);

-- Table: Course
CREATE TABLE Course
(
    courseID int NOT NULL IDENTITY,
    name varchar(50) NOT NULL,
    CONSTRAINT Course_pk PRIMARY KEY  (courseID)
);

-- Table: CourseContent
CREATE TABLE CourseContent
(
    courseID int NOT NULL,
    lessonID int NOT NULL,
    position int NOT NULL,
    CONSTRAINT CourseContent_pk PRIMARY KEY  (courseID,lessonID)
);

-- Table: Lesson
CREATE TABLE Lesson
(
    lessonID int NOT NULL IDENTITY,
    name varchar(50) NOT NULL,
    CONSTRAINT Lesson_pk PRIMARY KEY  (lessonID)
);

-- Table: LessonContent
CREATE TABLE LessonContent
(
    lessonID int NOT NULL,
    activityID int NOT NULL,
    position int NOT NULL,
    CONSTRAINT LessonContent_pk PRIMARY KEY  (lessonID,activityID)
);

-- foreign keys
-- Reference: Activity_ActivityType (table: Activity)
ALTER TABLE Activity ADD CONSTRAINT Activity_ActivityType
    FOREIGN KEY (activityTypeID)
    REFERENCES ActivityType (activityTypeID);

-- Reference: CourseContent_Course (table: CourseContent)
ALTER TABLE CourseContent ADD CONSTRAINT CourseContent_Course
    FOREIGN KEY (courseID)
    REFERENCES Course (courseID);

-- Reference: CourseContent_Lesson (table: CourseContent)
ALTER TABLE CourseContent ADD CONSTRAINT CourseContent_Lesson
    FOREIGN KEY (lessonID)
    REFERENCES Lesson (lessonID);

-- Reference: LessonContent_Activity (table: LessonContent)
ALTER TABLE LessonContent ADD CONSTRAINT LessonContent_Activity
    FOREIGN KEY (activityID)
    REFERENCES Activity (activityID);

-- Reference: LessonContent_Lesson (table: LessonContent)
ALTER TABLE LessonContent ADD CONSTRAINT LessonContent_Lesson
    FOREIGN KEY (lessonID)
    REFERENCES Lesson (lessonID);

-- End of file.

