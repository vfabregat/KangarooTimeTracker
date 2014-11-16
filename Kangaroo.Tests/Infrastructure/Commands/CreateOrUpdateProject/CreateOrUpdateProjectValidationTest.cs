using System;
using Kangaroo.Infrastructure;
using Kangaroo.Infrastructure.Commands;
using Kangaroo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kangaroo.Utils;
using System.Linq;

namespace Kangaroo.Tests.Infrastructure.Commands
{
    public class CreateOrUpdateProjectValidationTest
    {
        [TestClass]
        public class Given_a_valid_command_then_project_the_validation_is_valid : BDDTest
        {
            Project project;
            CreateOrUpdateProjectValidation SUT;

            protected override void Given()
            {
                project = new Project();
                var session = new Mock<ISession>();

                SUT = new CreateOrUpdateProjectValidation(session.Object);


                project.Name = "Project 1";
            }

            [TestMethod]
            public void TheProjectCommandIsValid()
            {
                var result = SUT.Validate(new CreateOrUpdateProject(project));

                Assert.IsTrue(result.IsValid);
            }
        }

        [TestClass]
        public class Given_a_valid_when_there_a_project_with_the_same_name_then_project_the_validation_is_invalid : BDDTest
        {
            Project projectToAdd;
            CreateOrUpdateProjectValidation SUT;
            MockSession session;
            string ExistingProject = "ExistingProject";
            protected override void Given()
            {
                projectToAdd = new Project();
                session = new MockSession();
                
                SUT = new CreateOrUpdateProjectValidation(session);

                projectToAdd.Name = ExistingProject;
            }

            protected override void When()
            {
                session.Add(new Project(){
                    Name = ExistingProject
                });
            }

            [TestMethod]
            public void TheProjectIsDuplicated()
            {
                var result = SUT.Validate(new CreateOrUpdateProject(projectToAdd));

                Assert.IsFalse(result.IsValid);
                Assert.AreEqual(1, result.Errors.Count);
            }

            [TestMethod]
            public void TheProjectNameIsEmpty()
            {
                projectToAdd.Name = string.Empty;
                var result = SUT.Validate(new CreateOrUpdateProject(projectToAdd));

                Assert.IsFalse(result.IsValid);
                Assert.AreEqual(1, result.Errors.Count);
            }
            [TestMethod]
            public void TheProjectNameIsNull()
            {
                projectToAdd.Name = null;
                var result = SUT.Validate(new CreateOrUpdateProject(projectToAdd));

                Assert.IsFalse(result.IsValid);
                Assert.AreEqual(1, result.Errors.Count);
            }
        }
    }
}