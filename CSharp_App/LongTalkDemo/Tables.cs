using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LongTalkDemo.Tables
{
    class TestInstances
    {
        public static List<Tables.Base> GetByName(string name)
        {
            switch(name)
            {
                case nameof(Tables.Drug): return new List<Tables.Base> { Instance.Drug };
                case nameof(Tables.Individual): return new List<Tables.Base> { Instance.Individual };
                case nameof(Tables.Staff): return new List<Tables.Base> { Instance.Staff };
                case nameof(Tables.Assay): return new List<Tables.Base> { Instance.Assay };
                case nameof(Tables.AssayPlate): return new List<Tables.Base> { Instance.AssayPlate };
                case nameof(Tables.AssayPlateDesign): return new List<Tables.Base> { Instance.AssayPlateDesign };
                case nameof(Tables.AssayPlateWell): return new List<Tables.Base>(Instance.AssayPlateWell);
                default: throw new ArgumentException(nameof(name));
            }
        }

        public static List<Tables.Base> GetByType<T>()
            where T: Tables.Base
        {
            var name = typeof(T).Name;
            return GetByName(name);
        }

        public static TestInstances Instance = new TestInstances();

        public TestInstances()
        {
            Drug = new Drug { DrugId = 1, Name = "Drug 1" };
            Individual = new Individual { IndividualId = 1, Name = "ABC 123" };
            Staff = new Staff
            {
                StaffId = 1, FirstName = "John Smith",
            };
            AssayPlateWell = new List<AssayPlateWell>
            {
                new AssayPlateWell
                {
                    AssayPlateWellId = 1, StartingConcentration = 10, Dilutionfactor = 5, Row = 'A', Column = 1,
                    AssayPlate = AssayPlate,
                    Individual = Individual,
                    Drug = Drug
                },
                new AssayPlateWell
                {
                    AssayPlateWellId = 2, Row = 'A', Column = 1,
                    AssayPlate = AssayPlate,
                    Individual = Individual,
                }
            };
            AssayPlateDesign = new AssayPlateDesign
            {
                TreatmentAssayPlateWell = AssayPlateWell[0],
                ControlAssayPlateWell = AssayPlateWell[1]
            };
            AssayPlate = new AssayPlate
            {
                AssayPlateId = 1,
                Barcode = "ABA042015",
                MeasurementDate = DateTime.Now,
                AssayPlateWells = AssayPlateWell
            };
            Assay = new Assay
            {
                AssayId = 1, SetupDate = DateTime.Now, Staff = Staff,
                AssayPlates = new List<AssayPlate> { AssayPlate }
            };

            AssayPlate.Assay = Assay;
            Staff.Assays = new List<Assay> { Assay };
        }

        public Drug Drug;
        public Individual Individual;
        public Staff Staff;
        public Assay Assay;
        public AssayPlate AssayPlate;
        public List<AssayPlateWell> AssayPlateWell;
        public AssayPlateDesign AssayPlateDesign;
    }

    public abstract class Base
    {
        private static Regex _capsToUnderscopeRegex = new Regex(@"\B[A-Z]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public string TableName => _capsToUnderscopeRegex.Replace(GetType().Name, match => $"_{match.Value.ToLowerInvariant()}").ToLowerInvariant();
        public abstract string GetId();
    }

    public class Drug: Base
    {
        public int DrugId { get; set; }
        public string Name { get; set; }

        public override string GetId() => Name;
    }
    public class Individual: Base
    {
        public int IndividualId { get; set; }
        public string Name { get; set; }
        public override string GetId() => Name;
    }
    public class Staff: Base
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }

        public List<Assay> Assays { get; set; }
        public override string GetId() => FirstName;
    }
    public class Assay: Base
    {
        public int AssayId { get; set; }
        public DateTime SetupDate { get; set; }


        public Staff Staff { get; set; }
        public List<AssayPlate> AssayPlates { get; set; }
        public override string GetId() => AssayId.ToString();
    }
    public class AssayPlate: Base
    {
        public int AssayPlateId { get; set; }
        public string Barcode { get; set; }
        public DateTime MeasurementDate { get; set; }

        public Assay Assay { get; set; }
        public List<AssayPlateWell> AssayPlateWells { get; set; }
        public override string GetId() => Barcode;
    }
    public class AssayPlateWell: Base
    {
        public int AssayPlateWellId { get; set; }
        public int StartingConcentration { get; set; }
        public int Dilutionfactor { get; set; }
        public char Row { get; set; }
        public int Column { get; set; }
        public decimal CellSizeSum { get; set; }
        public decimal CellSizeAvg { get; set; }
        public int LivingCellCount { get; set; }
        public decimal ProcessedArea { get; set; }
        public int ProcessDuration { get; set; }
        public int SampleVolume { get; set; }
        public int TcBf { get; set; }
        public int Cd { get; set; }
        public decimal Viability { get; set; }

        public AssayPlate AssayPlate { get; set; }
        public Individual Individual { get; set; }
        public Drug Drug { get; set; }
        public override string GetId() => AssayPlateWellId.ToString();
    }
    public class AssayPlateDesign: Base
    {
        public AssayPlateWell TreatmentAssayPlateWell { get; set; }
        public AssayPlateWell ControlAssayPlateWell { get; set; }
        public override string GetId() => $"{TreatmentAssayPlateWell} -> {ControlAssayPlateWell}";
    }
}
