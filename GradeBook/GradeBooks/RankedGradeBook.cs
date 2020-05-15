using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else 
                base.CalculateStatistics();
                
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStudentStatistics(name);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            int noOfStudents = Students.Count;
            if (noOfStudents < 5)
            {
                throw new InvalidOperationException("Need at least 5 students to calculate ranked grade");
            }

            //Get a list of average grades
            var averageGrades = new List<double>();
            foreach (Student student in Students)
            {
                averageGrades.Add(student.AverageGrade);
            }

            //Sort the average grades into descending order
            averageGrades.Sort();
            averageGrades.Reverse();
            
            //Determine where in the ordered list this average grade would sit
            int gradePosition = 0;
            for(; gradePosition < averageGrades.Count; gradePosition++)
            {
                if (averageGrade >= averageGrades[gradePosition])
                {
                    break;
                }
            }

            double gradeBoundary = noOfStudents / 5;
            if (gradePosition < gradeBoundary)
                return 'A';
            else if (gradePosition < gradeBoundary * 2)
                return 'B';
            else if (gradePosition < gradeBoundary * 3)
                return 'C';
            else if (gradePosition < gradeBoundary * 4)
                return 'D';
            else
                return 'F';

        }

        
    }
}
