using System;
using System.Linq;
using NUnit.Framework;
using ProtoCore.DSASM;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Utils;
using ProtoFFI;
namespace ProtoTest.UtilsTests
{
    [TestFixture]
    public class ArrayUtilsTest
    {
        public ProtoCore.Core core;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
            core = new ProtoCore.Core(new ProtoCore.Options());
            core.Executives.Add(ProtoCore.Language.kAssociative, new ProtoAssociative.Executive(core));
            core.Executives.Add(ProtoCore.Language.kImperative, new ProtoImperative.Executive(core));
            //DLLFFIHandler.Env = ProtoFFI.CPPModuleHelper.GetEnv(); 
            //DLLFFIHandler.Register(FFILanguage.CPlusPlus, new ProtoFFI.PInvokeModuleHelper());
        }

        [Test]
        public void StackValueDiffTestDefect()
        {
            String code =
@"[Imperative]
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            Assert.IsTrue(svA.metaData.type != svB.metaData.type);
        }

        [Test]
        public void StackValueDiffTestUserDefined()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            Assert.IsTrue(svA.metaData.type != svB.metaData.type);
        }

        [Test]
        public void StackValueDiffTestProperty01()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            Assert.IsTrue(svA.metaData.type != svB.metaData.type);
        }

        [Test]
        public void StackValueDiffTestProperty02()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("b");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("c");
            Assert.IsTrue(svA.metaData.type != svB.metaData.type);
        }

        [Test]
        public void TestArrayLayerStatsSimple()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            var dict = ProtoCore.Utils.ArrayUtils.GetTypeStatisticsForLayer(svA, core);
            Assert.IsTrue(dict[dict.Keys.First()] == 3);
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            var dict2 = ProtoCore.Utils.ArrayUtils.GetTypeStatisticsForLayer(svB, core);
            Assert.IsTrue(dict2[dict2.Keys.First()] == 4);
            ProtoCore.DSASM.StackValue svC = mirror.GetRawFirstValue("c");
            var dict3 = ProtoCore.Utils.ArrayUtils.GetTypeStatisticsForLayer(svC, core);
            Assert.IsTrue(dict3[dict3.Keys.First()] == 2);
            Assert.IsTrue(dict3[dict3.Keys.Last()] == 1);

            // Assert.IsTrue((Int64)o.Payload == 5);
        }

        [Test]
        public void TestArrayRankSimple()
        {
            String code =
@"a;b;c;d;e;
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            ProtoCore.DSASM.StackValue svC = mirror.GetRawFirstValue("c");
            ProtoCore.DSASM.StackValue svD = mirror.GetRawFirstValue("d");
            ProtoCore.DSASM.StackValue svE = mirror.GetRawFirstValue("e");
            var a = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svA, core);
            Assert.IsTrue(a == 1);
            var b = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svB, core);
            Assert.IsTrue(b == 1);
            var c = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svC, core);
            Assert.IsTrue(c == 1);
            var d = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svD, core);
            Assert.IsTrue(d == 2);
            var e = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svE, core);
            Assert.IsTrue(e == 2);
            // Assert.IsTrue((Int64)o.Payload == 5);
        }

        [Test]
        public void TestArrayRankJagged()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            ProtoCore.DSASM.StackValue svC = mirror.GetRawFirstValue("c");
            ProtoCore.DSASM.StackValue svD = mirror.GetRawFirstValue("d");
            ProtoCore.DSASM.StackValue svE = mirror.GetRawFirstValue("e");
            var a = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svA, core);
            Assert.IsTrue(a == 2);
            var b = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svB, core);
            Assert.IsTrue(b == 2);
            var c = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svC, core);
            Assert.IsTrue(c == 3);
            var d = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svD, core);
            Assert.IsTrue(d == 2);
            var e = ProtoCore.Utils.ArrayUtils.GetMaxRankForArray(svE, core);
            Assert.IsTrue(e == 4);
            // Assert.IsTrue((Int64)o.Payload == 5);
        }

        [Test]
        public void TestArrayGetCommonSuperType()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue svAAA = mirror.GetRawFirstValue("tAAA");
            ClassNode superAAA = ArrayUtils.GetGreatestCommonSubclassForArray(svAAA, core);
            Assert.IsTrue(superAAA.name == "A");
            StackValue svAAB = mirror.GetRawFirstValue("tAAB");
            ClassNode superAAB = ArrayUtils.GetGreatestCommonSubclassForArray(svAAB, core);
            Assert.IsTrue(superAAB.name == "A");
            StackValue svAAC = mirror.GetRawFirstValue("tAAC");
            ClassNode superAAC = ArrayUtils.GetGreatestCommonSubclassForArray(svAAC, core);
            Assert.IsTrue(superAAC.name == "A");
            StackValue svABA = mirror.GetRawFirstValue("tABA");
            ClassNode superABA = ArrayUtils.GetGreatestCommonSubclassForArray(svABA, core);
            Assert.IsTrue(superABA.name == "A");
            StackValue svABB = mirror.GetRawFirstValue("tABB");
            ClassNode superABB = ArrayUtils.GetGreatestCommonSubclassForArray(svABB, core);
            Assert.IsTrue(superABB.name == "A");
            StackValue svABC = mirror.GetRawFirstValue("tABC");
            ClassNode superABC = ArrayUtils.GetGreatestCommonSubclassForArray(svABC, core);
            Assert.IsTrue(superABC.name == "A");
            StackValue svACA = mirror.GetRawFirstValue("tACA");
            ClassNode superACA = ArrayUtils.GetGreatestCommonSubclassForArray(svACA, core);
            Assert.IsTrue(superACA.name == "A");
            StackValue svACB = mirror.GetRawFirstValue("tACB");
            ClassNode superACB = ArrayUtils.GetGreatestCommonSubclassForArray(svACB, core);
            Assert.IsTrue(superACB.name == "A");
            StackValue svACC = mirror.GetRawFirstValue("tACC");
            ClassNode superACC = ArrayUtils.GetGreatestCommonSubclassForArray(svACC, core);
            Assert.IsTrue(superACC.name == "A");
            //----
            StackValue svBAA = mirror.GetRawFirstValue("tBAA");
            ClassNode superBAA = ArrayUtils.GetGreatestCommonSubclassForArray(svBAA, core);
            Assert.IsTrue(superBAA.name == "A");
            StackValue svBAB = mirror.GetRawFirstValue("tBAB");
            ClassNode superBAB = ArrayUtils.GetGreatestCommonSubclassForArray(svBAB, core);
            Assert.IsTrue(superBAB.name == "A");
            StackValue svBAC = mirror.GetRawFirstValue("tBAC");
            ClassNode superBAC = ArrayUtils.GetGreatestCommonSubclassForArray(svBAC, core);
            Assert.IsTrue(superBAC.name == "A");
            StackValue svBBA = mirror.GetRawFirstValue("tBBA");
            ClassNode superBBA = ArrayUtils.GetGreatestCommonSubclassForArray(svBBA, core);
            Assert.IsTrue(superBBA.name == "A");
            StackValue svBBB = mirror.GetRawFirstValue("tBBB");
            ClassNode superBBB = ArrayUtils.GetGreatestCommonSubclassForArray(svBBB, core);
            Assert.IsTrue(superBBB.name == "B");
            StackValue svBBC = mirror.GetRawFirstValue("tBBC");
            ClassNode superBBC = ArrayUtils.GetGreatestCommonSubclassForArray(svBBC, core);
            Assert.IsTrue(superBBC.name == "B");
            StackValue svBCA = mirror.GetRawFirstValue("tBCA");
            ClassNode superBCA = ArrayUtils.GetGreatestCommonSubclassForArray(svBCA, core);
            Assert.IsTrue(superBCA.name == "A");
            StackValue svBCB = mirror.GetRawFirstValue("tBCB");
            ClassNode superBCB = ArrayUtils.GetGreatestCommonSubclassForArray(svBCB, core);
            Assert.IsTrue(superBCB.name == "B");
            StackValue svBCC = mirror.GetRawFirstValue("tBCC");
            ClassNode superBCC = ArrayUtils.GetGreatestCommonSubclassForArray(svBCC, core);
            Assert.IsTrue(superBCC.name == "B");
            //----
            StackValue svCAA = mirror.GetRawFirstValue("tCAA");
            ClassNode superCAA = ArrayUtils.GetGreatestCommonSubclassForArray(svCAA, core);
            Assert.IsTrue(superCAA.name == "A");
            StackValue svCAB = mirror.GetRawFirstValue("tCAB");
            ClassNode superCAB = ArrayUtils.GetGreatestCommonSubclassForArray(svCAB, core);
            Assert.IsTrue(superCAB.name == "A");
            StackValue svCAC = mirror.GetRawFirstValue("tCAC");
            ClassNode superCAC = ArrayUtils.GetGreatestCommonSubclassForArray(svCAC, core);
            Assert.IsTrue(superCAC.name == "A");
            StackValue svCBA = mirror.GetRawFirstValue("tCBA");
            ClassNode superCBA = ArrayUtils.GetGreatestCommonSubclassForArray(svCBA, core);
            Assert.IsTrue(superCBA.name == "A");
            StackValue svCBB = mirror.GetRawFirstValue("tCBB");
            ClassNode superCBB = ArrayUtils.GetGreatestCommonSubclassForArray(svCBB, core);
            Assert.IsTrue(superCBB.name == "B");
            StackValue svCBC = mirror.GetRawFirstValue("tCBC");
            ClassNode superCBC = ArrayUtils.GetGreatestCommonSubclassForArray(svCBC, core);
            Assert.IsTrue(superCBC.name == "B");
            StackValue svCCA = mirror.GetRawFirstValue("tCCA");
            ClassNode superCCA = ArrayUtils.GetGreatestCommonSubclassForArray(svCCA, core);
            Assert.IsTrue(superCCA.name == "A");
            StackValue svCCB = mirror.GetRawFirstValue("tCCB");
            ClassNode superCCB = ArrayUtils.GetGreatestCommonSubclassForArray(svCCB, core);
            Assert.IsTrue(superCCB.name == "B");
            StackValue svCCC = mirror.GetRawFirstValue("tCCC");
            ClassNode superCCC = ArrayUtils.GetGreatestCommonSubclassForArray(svCCC, core);
            Assert.IsTrue(superCCC.name == "C");
        }

        [Test]
        public void Defect_TestArrayGetCommonSuperType()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue svABC = mirror.GetRawFirstValue("tABC");
            ClassNode superABC = ArrayUtils.GetGreatestCommonSubclassForArray(svABC, core);
            Assert.IsTrue(superABC.name == "A");
            StackValue svABD = mirror.GetRawFirstValue("tABD");
            ClassNode superABD = ArrayUtils.GetGreatestCommonSubclassForArray(svABD, core);
            Assert.IsTrue(superABD.name == "A");
            StackValue svACD = mirror.GetRawFirstValue("tACD");
            ClassNode superACD = ArrayUtils.GetGreatestCommonSubclassForArray(svACD, core);
            Assert.IsTrue(superABD.name == "A");
            StackValue svBCD = mirror.GetRawFirstValue("tBCD");
            ClassNode superBCD = ArrayUtils.GetGreatestCommonSubclassForArray(svBCD, core);
            Assert.IsTrue(superBCD.name == "A");
            StackValue svAB = mirror.GetRawFirstValue("tAB");
            ClassNode superAB = ArrayUtils.GetGreatestCommonSubclassForArray(svAB, core);
            Assert.IsTrue(superAB.name == "A");
            StackValue svAD = mirror.GetRawFirstValue("tAD");
            ClassNode superAD = ArrayUtils.GetGreatestCommonSubclassForArray(svAD, core);
            Assert.IsTrue(superAD.name == "A");
            StackValue svBC = mirror.GetRawFirstValue("tBC");
            ClassNode superBC = ArrayUtils.GetGreatestCommonSubclassForArray(svBC, core);
            Assert.IsTrue(superBC.name == "A");
            StackValue svBD = mirror.GetRawFirstValue("tBD");
            ClassNode superBD = ArrayUtils.GetGreatestCommonSubclassForArray(svBD, core);
            Assert.IsTrue(superBD.name == "A");
            StackValue svCD = mirror.GetRawFirstValue("tCD");
            ClassNode superCD = ArrayUtils.GetGreatestCommonSubclassForArray(svCD, core);
            Assert.IsTrue(superCD.name == "C");
        }

        [Test]
        [Category("Method Resolution")]
        public void Defect_TestArrayGetCommonSuperType_2_EmptyArray()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue svABC = mirror.GetRawFirstValue("tABC");
            ClassNode superABC = ArrayUtils.GetGreatestCommonSubclassForArray(svABC, core);
            Assert.IsTrue(superABC.name == "A");
            StackValue svABD = mirror.GetRawFirstValue("tABD");
            ClassNode superABD = ArrayUtils.GetGreatestCommonSubclassForArray(svABD, core);
            Assert.IsTrue(superABD.name == "A");
            StackValue svACD = mirror.GetRawFirstValue("tACD");
            ClassNode superACD = ArrayUtils.GetGreatestCommonSubclassForArray(svACD, core);
            Assert.IsTrue(superABD.name == "A");
            StackValue svBCD = mirror.GetRawFirstValue("tBCD");
            ClassNode superBCD = ArrayUtils.GetGreatestCommonSubclassForArray(svBCD, core);
            Assert.IsTrue(superBCD.name == "A");
            StackValue svDD = mirror.GetRawFirstValue("tDD");
            ClassNode superDD = ArrayUtils.GetGreatestCommonSubclassForArray(svDD, core);
            Assert.IsTrue(superDD.name == "D");
            StackValue svE = mirror.GetRawFirstValue("tE");
            ClassNode superE = ArrayUtils.GetGreatestCommonSubclassForArray(svE, core);
            Assert.IsTrue(superE == null);
            //Assert.IsTrue(superE.name.Equals(""));
        }

        [Test]
        [Category("Method Resolution")]
        public void Defect_TestArrayGetCommonSuperType_3()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue svABCDEF = mirror.GetRawFirstValue("rABCDEF");
            ClassNode superABCDEF = ArrayUtils.GetGreatestCommonSubclassForArray(svABCDEF, core);
            Assert.IsTrue(superABCDEF.name == "A");
            StackValue svBCDEF = mirror.GetRawFirstValue("rBCDEF");
            ClassNode superBCDEF = ArrayUtils.GetGreatestCommonSubclassForArray(svBCDEF, core);
            Assert.IsTrue(superBCDEF.name == "A");
            StackValue svBH = mirror.GetRawFirstValue("rBH");
            ClassNode superBH = ArrayUtils.GetGreatestCommonSubclassForArray(svBH, core);
            Assert.IsTrue(superBH.name == "var");
        }

        [Test]
        public void IsArrayTest()
        {
            String code =
@"a;b;c;
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            ProtoCore.DSASM.StackValue svA = mirror.GetRawFirstValue("a");
            ProtoCore.DSASM.StackValue svB = mirror.GetRawFirstValue("b");
            ProtoCore.DSASM.StackValue svC = mirror.GetRawFirstValue("c");
            Assert.IsTrue(StackUtils.IsArray(svA));
            Assert.IsTrue(!StackUtils.IsArray(svB));
            Assert.IsTrue(StackUtils.IsArray(svC));
        }

        [Test]
        public void TestDepthCountOnJaggedArray()
        {
            String code =
                @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue a = mirror.GetRawFirstValue("a");
            StackValue b = mirror.GetRawFirstValue("b");
            StackValue x = mirror.GetRawFirstValue("x");
            int rankA = ArrayUtils.GetMaxRankForArray(a, core);
            Assert.IsTrue(rankA == 3);
            int rankB = ArrayUtils.GetMaxRankForArray(b, core);
            Assert.IsTrue(rankB == 2);
            int rankX = ArrayUtils.GetMaxRankForArray(x, core);
            Assert.IsTrue(rankX == 2);            /*
                         * 
                         */
        }

        [Test]
        public void Defect_OnDepthCount()
        {
            String code =
                @"
            ProtoScript.Runners.ProtoScriptTestRunner fsr = new ProtoScript.Runners.ProtoScriptTestRunner();
            ExecutionMirror mirror = fsr.Execute(code, core);
            StackValue a = mirror.GetRawFirstValue("a");
            int rankA = ArrayUtils.GetMaxRankForArray(a, core);
            Assert.IsTrue(rankA == 2);
        }
    }
}