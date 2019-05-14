using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace swi
{
    public class Validator
    {
        private XmlObjectList _xmlObjectList;

        public Validator(XmlObjectList xmlObjectList)
        {
            _xmlObjectList = xmlObjectList;
        }

        #region public methods

        public void ObjectValidate()
        {
            AreObjNamesUnique();

            foreach (var xmlObject in _xmlObjectList.XmlObjects.ToArray())
            {
                FieldValidate(xmlObject);

                if (!IsObjNamed(xmlObject) ||
                    !IsStringPrintable(xmlObject.Obj_Name) ||
                    ContainsDoubleQuote(xmlObject.Obj_Name) ||
                    !IsNumberOfFieldsValid(xmlObject, 1) )
                {
                    _xmlObjectList.XmlObjects.Remove(xmlObject);
                    continue;
                }
            }
        }

        public void FieldValidate(XmlObject xmlObject)
        {
            foreach (var field in xmlObject.Fields.ToArray())
            {
                if (!AreFieldElementsSupported(field) ||
                    !IsFieldNamed(field) ||
                    !IsTypeOfFieldSupported(field) ||
                    !IsFieldValued(field) ||
                    !IsFieldPrintable(field) ||
                    FieldContainsDoubleQuote(field)
                )
                {
                    xmlObject.Fields.Remove(field);
                    continue;
                }
            }
        }

        #endregion

        #region Object validate helpers

        private void AreObjNamesUnique()
        {
            var duplicatesGrouped = _xmlObjectList.XmlObjects
                .GroupBy(o => o.Obj_Name)
                .Where(c => c.Count() > 1);

            var duplicates = duplicatesGrouped
                .Select(d => d.Cast<XmlObject>()
                .ToList());

            foreach (var item in duplicates)
            {
                foreach (var obj in item)
                {
                    _xmlObjectList.XmlObjects.Remove(obj);
                }
            }
        }

        private bool IsObjNamed(XmlObject xmlObject)
        {
            if (string.IsNullOrEmpty(xmlObject.Obj_Name))
            {
                return false;
            }
            return true;
        }

        private bool IsNumberOfFieldsValid(XmlObject xmlObject, int minNumberOfFields)
        {
            if (xmlObject.Fields.Count < minNumberOfFields)
            {
                return false;
            }
            return true;
        }

        private bool AreObjectElementsSupported(XmlObject xmlObject)
        {
            if (xmlObject.Unsupported != null)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Field validate helpers

        private bool AreFieldElementsSupported(XmlField xmlField)
        {
            if (xmlField.Unsupported != null)
            {
                return false;
            }
            return true;
        }

        private bool IsFieldNamed(XmlField xmlField)
        {
            if (string.IsNullOrEmpty(xmlField.Name))
            {
                return false;
            }
            return true;
        }

        private bool IsFieldValued(XmlField xmlField)
        {
            if (string.IsNullOrEmpty(xmlField.Value))
            {
                return false;
            }
            return true;
        }

        private bool IsTypeOfFieldSupported(XmlField xmlField)
        {
            if (xmlField.Type == null)
            {
                return false;
            }

            switch (xmlField.Type.ToLower())
            {
                case "string":
                    break;
                case "int":
                    if (!int.TryParse(xmlField.Value, out int number))
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }

        private bool FieldContainsDoubleQuote(XmlField xmlField)
        {
            if (ContainsDoubleQuote(xmlField.Name) ||
                ContainsDoubleQuote(xmlField.Value))
            {
                return true;
            }
            return false;
        }

        private bool IsFieldPrintable(XmlField xmlField)
        {
            if (!IsStringPrintable(xmlField.Name) ||
                !IsStringPrintable(xmlField.Value))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region string methods


        private bool ContainsDoubleQuote(string word)
        {
            if (word.Contains("\""))
            {
                return true;
            }
            return false;
        }

        private bool IsStringPrintable(string word)
        {
            if (Regex.IsMatch(word, @"\p{C}+"))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
