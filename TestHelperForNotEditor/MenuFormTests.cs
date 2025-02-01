using HelperForNotEditor;
using NUnit.Framework;

namespace TestHelperForNotEditor
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitializeComboBox_ShouldPopulateComboBoxWithOperationNames()
        {
            // Arrange
            var menuForm = new MenuForm();
            var comboBoxItems = menuForm.ComboBoxItems;
            // Assert
            Assert.AreEqual(menuForm.OperationFormMap.Count, comboBoxItems.Count); 
            foreach (var operation in menuForm.OperationFormMap)
            {
                Assert.IsTrue(comboBoxItems.Contains(operation.Value.Name)); 
            }
        }
    }
}