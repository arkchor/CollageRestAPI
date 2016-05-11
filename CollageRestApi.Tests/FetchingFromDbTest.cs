using System;
using System.Diagnostics;
using System.Linq;
using CollageRestAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollageRestApi.Tests
{
    [TestClass]
    public class FetchingFromDbTest
    {
        [TestMethod]
        public void FetchFromDb()
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == 109502);
            //Debug.WriteLine($"{student.Id} {student.FirstName} {student.Grades.Count}");
            //student.Grades.ForEach(grade => Debug.WriteLine($"{grade.Id} {grade.Value}"));
        }
    }
}
