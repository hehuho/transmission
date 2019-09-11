using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exercice_Quizz_API_Vierge.Client;
using System;
using Exercice_Quizz_API_Vierge.Model;
using System.Reflection;

namespace Exercice_Quizz_API_Test
{
    [TestClass]
    public class TestModelObject
    {
        Uri client = new Uri("http://localhost:55124");
        string path = "../../../Json/Questions.json";
        ApiClient testedClient;

        [TestMethod]
        public void TestModelObjetPropertyName()
        {
            // Arrange
            bool success = false;
            bool ExpectedResult = true;

            Question ComparisonTestedObject = new Question();
            PropertyInfo[] properties;

            properties = ComparisonTestedObject.GetType().GetProperties();
            string[] propertiesName = new string[3];

            //Act
            if (properties.Length != 0)
            {
                for (int i = 0; i < properties.Length; i++)
                    propertiesName[i] = properties[i].Name;

                if (propertiesName.Length == 3)
                    if (propertiesName[0] == "QuestionId" && propertiesName[1] == "QuestionIntitule" && propertiesName[2] == "Answer")
                        success = true;

            }
            else
                success = false;

            //Assert
            Assert.AreEqual(ExpectedResult, success, "Modèle objet incorrect !");

        }

        [TestMethod]
        public void TestModelObjetPropertyType()
        {
            // Arrange
            bool success = false;
            bool ExpectedResult = true;

            Question ComparisonTestedObject = new Question();
            PropertyInfo[] properties;

            properties = ComparisonTestedObject.GetType().GetProperties();
            string[] propertiesType = new string[3];

            //Act
            if (properties.Length != 0)
            {
                for (int i = 0; i < properties.Length; i++)
                    propertiesType[i] = properties[i].PropertyType.FullName;

                if (propertiesType.Length == 3)
                    if (propertiesType[0] == "System.Int32" && propertiesType[1] == "System.String" && propertiesType[2] == "System.String")
                        success = true;
                
            }
            else
                success = false;

            //Assert
            Assert.AreEqual(ExpectedResult, success, "Modèle objet incorrect !");

        }
    }
}
