using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using LongTalkDemo.Tables;

namespace LongTalkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tables = new Base[]
            {
                TestInstances.Instance.Drug,
                TestInstances.Instance.Individual,
                TestInstances.Instance.Staff,
                TestInstances.Instance.Assay,
                TestInstances.Instance.AssayPlate,
                TestInstances.Instance.AssayPlateDesign
            };

            foreach (Base table in tables)
            {
                Printer(table);
                Console.WriteLine("========================");
            }
        }

        private static Sorter Sorter = new Sorter();
        private static void Printer(object obj, int indent = 0)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj).Sort(Sorter))
            {
                Console.Write(new string(' ', indent));
                Console.WriteLine($"{descriptor.Name} = '{descriptor.GetValue(obj)}'");

                if (descriptor.PropertyType.IsGenericType
                    && descriptor.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    foreach(var value in (IList)descriptor.GetValue(obj))
                    {
                        Printer(value, indent + 4);
                    }
                }
            }
        }
    }

    class Sorter : IComparer
    {
        public int Compare(object x, object y)
        {
            return (x as PropertyDescriptor)?.Name == "TableName"
                ? -1
                : 1;
        }
    }
}
