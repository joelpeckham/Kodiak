use kodiakDB;

select * from Activity

DELETE FROM ActivityType WHERE [Description] = 'Javascript Exercise';

insert into ActivityType values ('prompt', 'Text Prompt');
insert into ActivityType values ('javascript', 'Javascript Exercise');