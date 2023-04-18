using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace edu_services.Domain
{
    /// <summary>
    /// The classroom object a generic Teacher & list of generic Students. Helper methods have been added to facilitate adding each & to retrieve the roster.
    /// </summary>
    /// <typeparam name="T">Generic type for a Teacher</typeparam>
    /// <typeparam name="S">Generic type for a Student</typeparam>
    public class Classroom
    {
        public Classroom()
        {
            Students = new List<Student>();
        }

        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        /// <summary>
        /// Adds a teacher to the classroom.
        /// </summary>
        /// <param name="teacher">The teacher object</param>
        public async Task AddTeacherAsync(Teacher teacher)
        {
            if (Teacher != null)
            {
                throw new InvalidOperationException("A teacher has already been assigned to the classroom");
            }

            Teacher = teacher;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Adds a student to the classroom.
        /// </summary>
        /// <param name="student">The student object</param>
        public async Task AddStudentsAsync(List<Student> students)
        {
            if (Teacher == null)
            {
                throw new InvalidOperationException("A teacher must be assigned to the classroom before students can be added");
            }

            Students.AddRange(students);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Returns the representations of the teachers & students.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetRosterAsync()
        {
            if (Teacher == null)
                throw new Exception("Classroom contains no teacher.");

            if (Students == null || Students.Count > 3)
                throw new Exception("Classroom does not contain 3 students.");


            var roster = new List<string> { $"Teacher: {Teacher.Name}" };
            roster.AddRange(Students.Select(s => $"Student: {s.Name}"));

            return await Task.FromResult(roster);
        }
    }
}
