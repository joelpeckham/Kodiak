using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages
{
    

    public class IndexModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public IndexModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }


        public IList<Course> CourseList { get; set; }
        public IList<CourseContent> CourseContentList { get; set; }
        public IList<Lesson> LessonList { get; set; }
        public IList<LessonContent> LessonContentList { get; set; }
        public IList<Activity> ActivityList { get; set; }

        public class CourseLesson
        {
            public string name { get; set; }
            public int position { get; set; }
            public int lessonID { get; set; }
            public int courseID { get; set; }
        }        
        public List<CourseLesson> CourseLessons { get; set; }
        public class LessonActivity
        {
            public string name { get; set; }
            public int position { get; set; }
            public int lessonID { get; set; }
            public int activityID { get; set; }
        }
        public List<LessonActivity> LessonActivities { get; set; }
        public DbSet<Course> CourseObj {get; set;}
        public DbSet<Lesson> LessonObj { get; set;}
        public Course CourseItem { get; set; }
        public Lesson LessonItem { get; set; }

        public string lastClickedTable;
        public async Task OnGetAsync(string tableName, string idValue)
        {
            CourseList = await _context.Course.ToListAsync();
            CourseContentList = await _context.CourseContent.ToListAsync();
            LessonList = await _context.Lesson.ToListAsync();
            LessonContentList = await _context.LessonContent.ToListAsync();
            ActivityList = await _context.Activity.ToListAsync();

            if (tableName == "Course"){
                CourseItem = await _context.Course.FindAsync(Convert.ToInt32(idValue));
                CourseObj = _context.Course;
                lastClickedTable = "Course";
                var db = _context;
                {
                    var person = (from p in db.Lesson
                                  join e in db.CourseContent
                                  on p.LessonId equals e.LessonId
                                  where e.CourseId == Convert.ToInt32(idValue)
                                  select new CourseLesson
                                  {
                                      name = p.Name,
                                      position = e.Position,
                                      lessonID = e.LessonId,
                                      courseID = e.CourseId
                                  }).ToList();

                CourseLessons = person;
            }
            }
            if (tableName == "Lesson"){
                LessonItem = await _context.Lesson.FindAsync(Convert.ToInt32(idValue));
                LessonObj = _context.Lesson;
                lastClickedTable = "Lesson";
                var db = _context;
                {
                    var person = (from p in db.Activity
                                  join e in db.LessonContent
                                  on p.ActivityId equals e.ActivityId
                                  where e.LessonId == Convert.ToInt32(idValue)
                                  select new LessonActivity
                                  {
                                      name = p.Name,
                                      position = e.Position,
                                      lessonID = e.LessonId,
                                      activityID = e.ActivityId
                                  }).ToList();

                    LessonActivities = person;
                }
            }
        }

        private async Task deleteFromTable(string tableName, string idString){
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM "+tableName+" WHERE "+tableName+"ID in (" + idString + ")");
        }
        private async Task deleteCourseContent(string courseID, string lessonID){
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM CourseContent WHERE courseID = {Convert.ToInt32(courseID)} and lessonID = {Convert.ToInt32(lessonID)}");
        }
        private async Task deleteLessonContent(string lessonID, string activityID)
        {
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM lessonContent WHERE lessonID = {Convert.ToInt32(lessonID)} and activityID = {Convert.ToInt32(activityID)}");
        }
        private async Task addNameToTable(string tableName, string idString)
        {
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO "+tableName+" ([name]) VALUES ('"+idString+"')");
        }
        private async Task addActivity(string typeID, string name, string data)
        {
            await _context.Database.ExecuteSqlRawAsync("insert into Activity(activityTypeID, [name], [data]) values('"+typeID+"', '"+name+"', '"+data+"')");
        }
        private async Task AddLessonToCourse(string courseID, string lessonID)
        {
            var pos = 1;
            try
            {
                pos = _context.CourseContent.ToList().Max(v => v.Position) + 1;
            }
            catch{
                pos = 1;
            }
            await _context.Database.ExecuteSqlRawAsync($"insert into CourseContent (courseID, lessonID, position) values ({Convert.ToInt32(courseID)},{Convert.ToInt32(lessonID)},{pos})");
        }
        private async Task AddActivityToLesson(string lessonID, string activityID)
        {
            var pos = 1;
            try
            {
                pos = _context.LessonContent.ToList().Max(v => v.Position) + 1;
            }
            catch
            {
                pos = 1;
            }
            await _context.Database.ExecuteSqlRawAsync($"insert into LessonContent (lessonID, activityID, position) values ({Convert.ToInt32(lessonID)},{Convert.ToInt32(activityID)},{pos})");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var actionType = Request.Form["actionType"];
            if (actionType == "Delete") {
                await deleteFromTable(Request.Form["tableName"], Request.Form["idValue"]);
                return RedirectToPage("Index");
            } 
            else if (actionType == "AddCourseLesson"){
                await addNameToTable(Request.Form["tableName"], Request.Form["idValue"]);
                return RedirectToPage("Index");
            }
            else if (actionType == "AddActivity")
            {
                await addActivity(Request.Form["TypeID"], Request.Form["Name"], "");
                return RedirectToPage("Index");
            }
            else if (actionType == "AddLessonToCourse")
            {
                await AddLessonToCourse(Request.Form["CourseID"], Request.Form["LessonID"]);
                return RedirectToPage("Index");
            }
            else if (actionType == "AddActivityToLesson")
            {
                await AddActivityToLesson(Request.Form["LessonID"], Request.Form["ActivityID"]);
                return RedirectToPage("Index");
            }
            else if (actionType == "DeleteCourseContent")
            {
                await deleteCourseContent(Request.Form["CourseID"], Request.Form["LessonID"]);
                return RedirectToPage("Index");
            }
            else if (actionType == "DeleteLessonContent")
            {
                await deleteLessonContent(Request.Form["LessonID"], Request.Form["ActivityID"]);
                return RedirectToPage("Index");
            }
            else{
                return RedirectToPage("Index");
            } 
        }
    }
}

