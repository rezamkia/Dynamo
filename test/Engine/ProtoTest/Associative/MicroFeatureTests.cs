using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Lang;
using ProtoTest.TD;
using ProtoTestFx.TD;
namespace ProtoTest.Associative
{
    public class MicroFeatureTests
    {
        public TestFrameWork thisTest = new TestFrameWork();
        readonly string testCasePath = Path.GetFullPath(@"..\..\..\Scripts\Associative\MicroFeatureTests\");
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAssignment01()
        {

            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("foo");
            Assert.IsTrue((Int64)o.Payload == 5);
        }

        [Test]
        public void TestAssignment02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Obj o = mirror.GetValue("foo");
            Assert.IsTrue((Int64)(mirror.GetValue("boo")).Payload == 5);
            Assert.IsTrue((Double)(mirror.GetValue("moo")).Payload == 7.2);
            Assert.IsTrue((Int64)(mirror.GetValue("scoo")).Payload == 11);
        }

        [Test]
        public void TestNull01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue(mirror.GetValue("x").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
            Assert.IsTrue(mirror.GetValue("y").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
            Assert.IsTrue(mirror.GetValue("a").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
            Assert.IsTrue(mirror.GetValue("b").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
            Assert.IsTrue(mirror.GetValue("c").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
        }

        [Test]
        public void TestNull02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue(mirror.GetValue("c").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
        }

        [Test]
        public void TestFunctions01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("test").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("test2").Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("test3").Payload == 20);
        }

        [Test]
        public void TestFunctions02()
        {
            String code =
@"        
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("temp").Payload == 7);
        }

        [Test]
        public void TestFunctionsOverload01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("test1").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("test2").Payload == 50);
        }

        [Test]
        public void TestFunctionsOverload02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 120);
            Assert.IsTrue((Int64)mirror.GetValue("j").Payload == 22);
        }

        [Test]
        public void TestFunctionsOverload03()
        {
            String code =
            @"class A
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("d");
            List<Obj> os = mirror.GetArrayElements(o);
            Assert.IsTrue(os.Count == 4);
            Assert.IsTrue((Int64)os[0].Payload == 1);
            Assert.IsTrue((Int64)os[1].Payload == 2);
            Assert.IsTrue((Int64)os[2].Payload == 3);
            Assert.IsTrue((Int64)os[3].Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 2);
            o = mirror.GetValue("e");
            os = mirror.GetArrayElements(o);
            Assert.IsTrue(os.Count == 4);
            Assert.IsTrue((Int64)os[0].Payload == 5);
            Assert.IsTrue((Int64)os[1].Payload == 6);
            Assert.IsTrue((Int64)os[2].Payload == 7);
            Assert.IsTrue((Int64)os[3].Payload == 8);
            Assert.IsTrue((Int64)mirror.GetValue("z").Payload == 2);
        }

        [Test]
        public void TestDynamicDispatch01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("t1").Payload == -2);
            Assert.IsTrue((Int64)mirror.GetValue("t2").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("t3").Payload == 100);
        }

        [Test]
        public void TestClasses01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("somevar").Payload == 10001);
            Assert.IsTrue((Int64)mirror.GetValue("another").Payload == 888888);
            Assert.IsTrue((Int64)mirror.GetValue("xx").Payload == 888888);
            Assert.IsTrue((Int64)mirror.GetValue("yy").Payload == 999999);
        }

        [Test]
        public void TestClasses02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 100);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 100);
        }

        [Test]
        public void TestClasses03()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 10);
        }

        [Test]
        public void TestClasses04()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("ax").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("bx").Payload == 2);
        }

        [Test]
        public void TestClasses05()
        {
            String code =
@"  
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x").Payload == 12);
        }

        [Test]
        public void TestClasses06()
        {
            String code =
@"  
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a1").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("a2").Payload == 5);
            Assert.IsTrue((Int64)mirror.GetValue("a3").Payload == 9);
            Assert.IsTrue((Int64)mirror.GetValue("a4").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("a5").Payload == 16);
        }

        [Test]
        public void TestClasses07()
        {
            String code =
@"  
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 64);
        }

        [Test]
        public void TestClasses08()
        {
            String code =
@"  
            Assert.Throws(typeof(ProtoCore.Exceptions.CompileErrorsOccured), () =>
            {
                ExecutionMirror mirror = thisTest.RunScriptSource(code);
            });
        }

        [Test]
        public void TestClassFunction01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("i", 160);
            thisTest.Verify("j", 320);
        }

        [Test]
        public void TestClassFunction02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 12);
        }

        [Test]
        public void TestClassFunction03()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 522);
        }

        [Test]
        public void TestClassFunction04()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("one").Payload == 5);
            Assert.IsTrue((double)mirror.GetValue("two").Payload == 100);
        }

        [Test]
        public void TestClassFunction05()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("v1").Payload == 3);
            Assert.IsTrue((double)mirror.GetValue("v2").Payload == 3);
            Assert.IsTrue((double)mirror.GetValue("v3").Payload == 3);
            Assert.IsTrue((double)mirror.GetValue("v4").Payload == 2);
            Assert.IsTrue((double)mirror.GetValue("v5").Payload == 1);
            Assert.IsTrue((double)mirror.GetValue("v6").Payload == 31);
            Assert.IsTrue((double)mirror.GetValue("v7").Payload == 21);
            Assert.IsTrue((double)mirror.GetValue("v8").Payload == 11);
        }

        [Test]
        public void TestClassFunction06()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("e", 0).Payload == 10);
            Assert.IsTrue((double)mirror.GetValue("f", 0).Payload == 20);
            Assert.IsTrue((double)mirror.GetValue("g", 0).Payload == 30);
        }

        [Test]
        public void TestClassFunction07()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("val").Payload == 60);
        }

        [Test]
        public void TestClassFunction08()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("l_sp_x").Payload == 3);
            Assert.IsTrue((double)mirror.GetValue("l_ep_x").Payload == 30.1);
            Assert.IsTrue((double)mirror.GetValue("l_sp_y").Payload == 2);
            Assert.IsTrue((double)mirror.GetValue("l_ep_y").Payload == 20.1);
            Assert.IsTrue((double)mirror.GetValue("l_sp_z").Payload == 1);
            Assert.IsTrue((double)mirror.GetValue("l_ep_z").Payload == 10.1);
        }

        [Test]
        public void TestClassFunction09()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x").Payload == 0);
            Assert.IsTrue((double)mirror.GetValue("y").Payload == 100);
            Assert.IsTrue((double)mirror.GetValue("z").Payload == 200);
        }

        [Test]
        public void TestClassFunction10()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void TestClassFunction11()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("xval").Payload == 10);
        }

        [Test]
        public void TestClassFunction12()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void TestClassFunction13()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x").Payload == 1);
            Assert.IsTrue((double)mirror.GetValue("y").Payload == 1);
            Assert.IsTrue((double)mirror.GetValue("z").Payload == 1);
            Assert.IsTrue((double)mirror.GetValue("h").Payload == 0);
        }

        [Test]
        [Category("JunToFix")]
        public void TestClassFunction14()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x").Payload == 11);
            Assert.IsTrue((double)mirror.GetValue("y").Payload == 11);
        }

        [Test]
        public void TestClassFunction15()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((double)mirror.GetValue("x", 0).Payload == 20);
            Assert.IsTrue((double)mirror.GetValue("y", 0).Payload == 30);
            Assert.IsTrue((double)mirror.GetValue("z", 0).Payload == 40);
        }

        [Test]
        public void TestClassFunction16()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void TestClassFunction17()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a", 0).Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("b", 0).Payload == 20);
        }

        [Test]
        public void TestStaticUpdate01()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("t", 0).Payload == 10);
        }

        [Test]
        public void TestStaticUpdate02()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t", new object[] { 1, 2 });
        }

        [Test]
        public void TestStaticProperty01()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("t1", 0).Payload == 3);
            Assert.IsTrue((Int64)mirror.GetValue("t2", 0).Payload == 3);
        }

        [Test]
        public void TestStaticProperty02()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b", 0).Payload == 2);
        }

        [Test]
        [Category("Method Resolution")]
        public void TestStaticFunction01()
        {
            string code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("d2", 0).Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("d3", 0).Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("f", 0).Payload == 11);
            Assert.IsTrue((Int64)mirror.GetValue("d4", 0).Payload == 5);
            Assert.IsTrue((Int64)mirror.GetValue("d5", 0).Payload == 5);
        }

        [Test]
        public void TestStaticMethodResolution()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("c");
            List<Obj> os = mirror.GetArrayElements(o);
            Assert.IsTrue(os.Count == 4);
            Assert.IsTrue((Int64)os[0].Payload == 1);
            Assert.IsTrue((Int64)os[1].Payload == 2);
            Assert.IsTrue((Int64)os[2].Payload == 3);
            Assert.IsTrue((Int64)os[3].Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("d").Payload == 9);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("v").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("w").Payload == 2);
        }

        [Test]
        public void TestClassNegative01()
        {
            Assert.Throws(typeof(ProtoCore.Exceptions.CompileErrorsOccured), () =>
            {
                thisTest.RunScriptSource(
    @"
                );
            });
        }

        [Test]
        public void TestTemporaryArrayIndexing01()
        {
            string code = @"t = {1,2,3,4}[3]; ";
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing02()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing03()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing04()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing05()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", new object[] { 2, 3, 4 });
        }

        [Test]
        public void TestTemporaryArrayIndexing06()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", new object[] { 2, 3, 4 });
        }

        [Test]
        public void TestTemporaryArrayIndexing07()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 7);
        }

        [Test]
        public void TestArray001()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 23);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 1002);
            Obj o = mirror.GetValue("a");
            ProtoCore.DSASM.Mirror.DsasmArray arr = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue((Int64)arr.members[0].Payload == 23);
            Assert.IsTrue((Int64)arr.members[1].Payload == 1002);
        }

        [Test]
        public void TestArray002()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 100);
        }

        [Test]
        public void TestArray003()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            ProtoCore.DSASM.Mirror.DsasmArray arr = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue((Int64)arr.members[0].Payload == 10);
            Assert.IsTrue((Int64)arr.members[1].Payload == 1);
            Assert.IsTrue((Int64)arr.members[2].Payload == 2);
        }

        [Test]
        public void TestIndexingIntoArray01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r2", 10);
            thisTest.Verify("r3", 10);
        }

        [Test]
        public void TestIndexingIntoArray02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", 5);
            thisTest.Verify("y", 2);
        }

        [Test]
        public void TestArrayOverIndexing01()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            TestFrameWork.VerifyRuntimeWarning(ProtoCore.RuntimeData.WarningID.kOverIndexing);
        }

        [Test]
        public void TestDynamicArray001()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            ProtoCore.DSASM.Mirror.DsasmArray arr = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue((Int64)arr.members[0].Payload == 10);
            Assert.IsTrue((Int64)arr.members[1].Payload == 20);
            Assert.IsTrue((Int64)arr.members[2].Payload == 100);
        }

        [Test]
        public void TestDynamicArray002()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("t");
            ProtoCore.DSASM.Mirror.DsasmArray arr = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue((Int64)arr.members[0].Payload == 100);
            Assert.IsTrue((Int64)arr.members[1].Payload == 200);
            Assert.IsTrue((Int64)arr.members[2].Payload == 300);
        }

        [Test]
        public void TestDynamicArray003()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
        }

        [Test]
        public void TestDynamicArray004()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 20);
        }

        [Test]
        public void TestDynamicArray005()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 40);
        }

        [Test]
        public void TestDynamicArray006()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 3);
            Assert.IsTrue((Int64)mirror.GetValue("d").Payload == 4);
        }

        [Test]
        public void TestDynamicArray007()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { null, null, null, 3 });
        }

        [Test]
        public void TestDynamicArray008()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { false });
        }

        [Test]
        public void TestDynamicArray009()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { false, null, null, 1 });
        }

        [Test]
        public void TestDynamicArray010()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { false, new object[] { null, new object[] { 3 } } });
        }

        [Test]
        public void TestDynamicArray011()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { new object[] { 1, 2 } });
        }

        [Test]
        public void TestDynamicArray012()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { 2 });
        }

        [Test]
        public void TestDynamicArray013()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { 2, null, 1 });
        }

        [Test]
        public void TestDynamicArray014()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { 100, 1, 2, null, 3 });
        }

        [Test]
        public void TestDynamicArray015()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { new object[] { 3 }, 1 });
        }

        [Test]
        public void TestDictionary01()
        {
            // Using string as a key
            String code = @"
a = {1, 2, 3};
a[""x""] = 42;
r = a [""x""];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary02()
        {
            // Double value can't be used as a key
            String code = @"
a = {1, 2, 3};
a[1.234] = 42;
r = a [1.3];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary03()
        {
            // Using boolean value as a key
            String code = @"
a = {};
a[true] = 42;
a[false] = 41;
r1 = a [1 == 1];
r2 = a [0 == 1];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", 42);
            thisTest.Verify("r2", 41);
        }

        [Test]
        public void TestDictionary04()
        {
            // Using character value as a key
            String code = @"
a = {};
a['x'] = 42;
r1 = a['x'];
r2 = a[""x""];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", 42);
            thisTest.Verify("r2", null);
        }

        [Test]
        public void TestDictionary05()
        {
            // Using class instance as a key 
            String code = @"
class A
{
}
a = A();
arr = {};
arr[a] = 41;
arr[a] = 42;
r = arr[a];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary06()
        {
            // Test replication on array indexing on LHS
            // using character as a key
            String code = @"
strs = {""x"", true, 'b'};
arr = {};
arr[strs] = {11, 13, 17};
r1 = arr[""x""];
r2 = arr[true];
r3 = arr['b'];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", 11);
            thisTest.Verify("r2", 13);
            thisTest.Verify("r3", 17);
        }

        [Test]
        public void TestDictionary07()
        {
            // Test replication on array indexing on RHS
            String code = @"
strs = {""x"", true, 'b'};
arr = {};
arr[strs] = {11, 13, 17};
values = arr[strs];
r1 = values[0];
r2 = values[1];
r3 = values[2];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", 11);
            thisTest.Verify("r2", 13);
            thisTest.Verify("r3", 17);
        }

        [Test]
        public void TestDictionary08()
        {
            // Test for 2D array
            String code = @"
arr = {{1, 2}, {3, 4}};
arr[1][""xyz""] = 42;
arr[1][true] = 42;
r1 = arr[1][""xyzxyzxyz""];
r2 = arr[1][true];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", null);
            thisTest.Verify("r2", 42);
        }

        [Test]
        public void TestDictionary09()
        {
            // Copy array should also copy key-value pairs
            String code = @"
a = {};
a[""xyz""] = 42;
b = a;
r = b[""xyz""];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary10()
        {
            // Key-value shouldn't be disposed after scope
            String code = @"
a = [Imperative]
{
    b = {};
    b[""xyz""] = 42;
    return = b;
}

r = a[""xyz""];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary11()
        {
            // Copy array should also copy key-value pairs
            String code = @"
a = {};
a[true] = 42;
b = a;
r = b[true];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary14()
        {
            // Copy array should also copy key-value pairs
            String code = @"
a = {};
a[true] = 42;
def foo(x: var[]..[])
{
    return = x[true];
}
r = foo(a);
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary12()
        {
            // Type conversion applied to values as well
            String code = @"
a:int[] = {1.1, 2.2, 3.3};
a[true] = 42.4;
r = a[true];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary13()
        {
            // Type conversion applied to values as well
            String code = @"
a = {1.1, 2.2, 3.3};
a[true] = 42.4;
b:int[] = a;
r = b[true];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary15()
        {
            // Test for-loop to get values
            String code = @"
a = {1, 2, 3};
a[true] = 42;
r = [Imperative]
{
    x = null;
    for (v in a)
    {
        x = v;
    }
    return = x;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary16()
        {
            // Test replication for function call
            String code = @"
a = {1, 2, 3};
a[true] = 42;

def foo(x) { return = x; }
r1 = foo(a);
r2 = r1[3];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r2", 42);
        }

        [Test]
        public void TestDictionary17()
        {
            // Test replication for function call
            String code = @"
a = {1, 2, 3};
a[true] = 21;
b = {1, 2, 3};
b[false] = 21;

def foo(x, y) { return = x + y; }
sum = foo(a, b);
r = sum[3];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary18()
        {
            // Test replication for array indexing
            String code = @"
a = {1, 2, 3};
a[true] = 42;
b = {};
b[a] = 1;
b[42] = 42;
c = b[a];
r = c[3];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 42);
        }

        [Test]
        public void TestDictionary19()
        {
            // Test builtin functions GetKeys() for array
            String code = @"
        a = {1, 2, 3};
a[true] = 41;
a[""x""] = ""foo"";
r = Count(GetKeys(a));
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 5);
        }

        [Test]
        public void TestDictionary20()
        {
            // Test builtin functions GetValues() for array
            String code = @"
        a = {1, 2, 3};
a[true] = 41;
a[""x""] = ""foo"";
r = Count(GetValues(a));
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 5);
        }

        [Test]
        public void TestDictionary21()
        {
            // Test builtin functions ContainsKey() for array
            String code = @"
        a = {1, 2, 3};
a[true] = 41;
a[""x""] = ""foo"";
r1 = ContainsKey(a, ""x"");
r2 = ContainsKey(a, true);
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", true);
            thisTest.Verify("r2", true);
        }

        [Test]
        public void TestDictionary22()
        {
            // Test builtin functions RemoveKey() for array
            String code = @"
        a = {1, 2, 3};
a[true] = 41;
a[""x""] = ""foo"";
r1 = RemoveKey(a, ""x"");
r2 = RemoveKey(a, true);
r3 = ContainsKey(a, ""x"");
r4 = ContainsKey(a, true);
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", true);
            thisTest.Verify("r2", true);
            thisTest.Verify("r3", false);
            thisTest.Verify("r4", false);
        }

        [Test]
        public void TestDictionary23()
        {
            // Test for-loop
            String code = @"
r = [Imperative]
{
    a = {1, 5, 7};
    a[""x""] = 9;
    a[true] = 11;
    x = 0; 
    for (v in a) 
    {
        x = x + v;
    }
    return = x;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 33);
        }

        [Test]
        public void TestDictionary24()
        {
            // Test for-loop
            String code = @"
r = [Imperative]
{
    a = {};
    x = 0;
    for (v in a) 
    {
        x = x + v;
    }
    return = x;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", 0);
        }

        [Test]
        public void TestDictionaryRegressMAGN337()
        {
            string code = @"
     a = { 1, 2, 3 };
            b = {""x"",""y""};
                
def foo(a1 : var[], b1 : var[])
            {

                a1[b1] = true;
                return =a1;
            }
z1 = foo(a, b);
r1=z1[""x""];
r2=z1[""y""];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", true);
            thisTest.Verify("r2", true);
        }


        [Test]
        public void TestDictionaryRegressMAGN619()
        {
            string code = @"
a[null]=5;
c=Count(a);
r = a[null];
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", 0);
            thisTest.Verify("r", 5);
        }

        [Test]
        public void TestArrayCopyAssignment01()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("z", 2);
        }

        [Test]
        public void TestArrayCopyAssignment02()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("z", 2);
        }

        [Test]
        public void TestDynamicArray016()
        {
            String code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { new object[] { 5 }, new object[] { 1, 2 }, new object[] { 3, 4 } });
        }

        [Test]
        public void TestArrayIndexReplication01()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { 1, 2, 2 });
        }

        [Test]
        public void TestArrayIndexReplication02()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("b", new object[] { 2, 3 });
        }

        [Test]
        public void TestDynamicArrayNegative01()
        {
            string code = @"
            thisTest.RunScriptSource(code, "");
            thisTest.Verify("x", null);
        }

        [Test]
        public void TestDynamicArrayNegative02()
        {
            string code = @"
            thisTest.RunScriptSource(code, "");
            thisTest.Verify("x", null);
        }

        [Test]
        public void TestReplicationGuidesOnFunctions01()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { 4, 5 });
            thisTest.Verify("b", new object[] { 5, 6 });
        }

        [Test]
        public void TestReplicationGuidesOnDotOps01()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", new object[] { 2, 3 });
            thisTest.Verify("y", new object[] { 3, 4 });
        }

        [Test]
        public void TestReplicationGuidesOnDotOps02()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("b", new object[] { 1, 1 });
        }

        [Test]
        public void TestReplicationGuidesOnDotOps03()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("b", new object[] { 1, 1 });
        }

        [Test]
        public void TestReplicationGuidesOnDotOps04()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", new object[] { 10, 10 });
            thisTest.Verify("z", new object[] { 10, 10 });
        }

        [Test]
        public void TestReplicationGuidesOnDotOps05()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", new object[] { 2, 3 });
            thisTest.Verify("z", new object[] { 3, 4 });
        }

        [Test]
        public void TestTypeArrayAssign4()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", new object[] { null, 2, 3 });
        }

        [Test]
        public void TestTypeArrayAssign5()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("b", new object[] { null, 2, null });
        }

        [Test]
        public void TestTypeArrayAssign6()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("a", null);
        }

        [Test]
        public void TestTypeArrayAssign_1467462()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", new object[] { 1, 2, 1, 2 });
        }

        [Test]
        public void NestedBlocks001()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetValue("a");
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 1);
        }
        [Ignore]
        public void BitwiseOp001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
        }
        [Ignore]
        public void BitwiseOp002()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 3);
        }
        [Ignore]
        public void BitwiseOp003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == -3);
        }
        [Ignore]
        public void BitwiseOp004()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", null);
        }

        [Test]
        public void LogicalOp001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("e", false);
        }

        [Test]
        public void LogicalOp002()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("e", true);
        }

        [Test]
        public void LogicalOp003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", false);
        }

        [Test]
        public void DoubleOp()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 5.0);
        }

        [Test]
        public void RangeExpr001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("d").Payload == 3);
            Assert.IsTrue((Int64)mirror.GetValue("e").Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("f").Payload == 5);
        }

        [Test]
        public void RangeExpr002()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 1.5);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 2.6);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 3.7);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 4.8);
        }

        [Test]
        public void RangeExpr003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 15.0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 13.5);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 12.0);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 10.5);
        }

        [Test]
        public void RangeExpr004()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 3.75);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 7.5);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 11.25);
            Assert.IsTrue((Double)mirror.GetValue("f").Payload == 15);
        }

        [Test]
        public void RangeExpr005()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 3.75);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 7.5);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 11.25);
            Assert.IsTrue((Double)mirror.GetValue("f").Payload == 15);
        }

        [Test]
        public void FunctionWithinConstr001()
        {
            String code =
                        @"                        
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a", 0).Payload == 5);
        }

        [Test]
        public void InlineCondition001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 10);
        }

        [Test]
        public void InlineCondition002()
        {
            String code =
                        @"	
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 20);
        }

        [Test]
        public void InlineCondition003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("z").Payload == 1);
        }

        [Test]
        public void InlineCondition004()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 11);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("z").Payload == 11);
        }
        [Ignore]
        public void PrePostFix001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 6);
        }
        [Ignore]
        public void PrePostFix002()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 5);
        }
        [Ignore]
        public void PrePostFix003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 8);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 7);
        }

        [Test]
        public void Modulo001()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 0);
        }

        [Test]
        public void Modulo002()
        {
            String code =
               @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
        }

        [Test]
        public void NegativeIndexOnCollection001()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 3);
        }

        [Test]
        public void NegativeIndexOnCollection002()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 3);
        }

        [Test]
        public void NegativeIndexOnCollection003()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 4);
        }

        [Test]
        public void PopListWithDimension()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("watch1", -2);
            thisTest.Verify("watch2", 3);
            thisTest.Verify("watch3", 3);
            thisTest.Verify("watch4", -3);
        }

        [Test]
        public void TestUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 10);
        }

        [Test]
        public void TestUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
        }

        [Test]
        public void TestUpdate03()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 40);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 40);
        }

        [Test]
        public void TestUpdateRedefinition01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 3);
        }

        [Test]
        public void TestUpdateRedefinition02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 10);
        }

        [Test]
        public void TestArrayUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 12);
        }

        [Test]
        public void TestFunctionUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 10);
        }

        [Test]
        [Category("JunToFix")]
        public void TestFunctionUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 10);
        }

        [Test]
        public void TestNoUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("length").Payload == 8);
        }

        [Test]
        public void TestPropertyUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 2);
        }
        // Comment Jun: Investigate how replicating setters have affected this update

        [Test]
        public void TestPropertyUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            //Assert.Fail("1467249 - Sprint25: rev 3468 : REGRESSION: class property update is not propagating");
            Assert.IsTrue((Int64)mirror.GetValue("t").Payload == 10);
        }

        [Test]
        public void TestPropertyUpdate03()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 2);
        }

        [Test]
        public void TestPropertyUpdate04()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("t").Payload == 10);
        }

        [Test]
        public void TestPropertyUpdate05()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("i").Payload == 1);
        }

        [Test]
        public void TestPropertyUpdate06()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("t").Payload == 12);
        }


        [Test]
        public void TestPropertyUpdate07()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("v").Payload == 15);
        }

        [Test]
        public void TestLHSUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("i").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetFirstValue("j").Payload == 10);
        }

        [Test]
        public void TestLHSUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("b").Payload == 10);
        }

        [Test]
        public void TestPropertyModificationInMethodUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("x").Payload == 10);
        }

        [Test]
        public void TestPropertyModificationInMethodUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("x").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetFirstValue("y").Payload == 20);
        }

        [Test]
        public void TestXLangUpdate01()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
        }

        [Test]
        public void TestXLangUpdate02()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 11);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 11);
        }

        [Test]
        public void TestXLangUpdate03()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 10);
            Assert.IsTrue((Int64)mirror.GetValue("d").Payload == 10);
        }

        [Test]
        public void TestXLangUpdate04()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 10);
        }

        [Test]
        public void TestGCRefCount()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void TestGCFFI001()
        {
            String code =
                @"
            code = string.Format("{0}\n{1}", "import(\"ProtoGeometry.dll\");", code);
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
        }

        [Test]
        public void TestGCRefCount002()
        {
            String code =
                @"
            code = string.Format("{0}\n{1}", "import(\"ProtoGeometry.dll\");", code);
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Obj o = mirror.GetFirstValue("x");
            Assert.IsTrue((Double)o.Payload == 5.0);
        }

        [Test]
        public void TestGlobalVariable()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetFirstValue("gx").Payload == 100);
        }

        [Test]
        public void TestNullFFI()
        {
            String code =
                @"
            code = string.Format("{0}\n{1}", "import(\"ProtoGeometry.dll\");", code);
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue(mirror.GetFirstValue("value").DsasmValue.optype == ProtoCore.DSASM.AddressType.Null);
        }

        [Test]
        public void TestAttributeOnClass()
        {
            string src = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Test, Visibility(1)]
class Point
{
	
[Test]
	public x : var;
	[Visibility(2)]
	public y : var;
	
	[Test, Visibility(1)]
	constructor Point()
	{
		x = 10; y = 10;
	}
	
[Test]
	public static def foo : int()
	{
		return = 10;
	}
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        public void TestAttributeOnGlobalFunction()
        {
            string src = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Test, Visibility(1)]
class Point
{
	
[Test]
	public x : var;
	[Visibility(2)]
	public y : var;
	
	[Test, Visibility(1)]
	constructor Point()
	{
		x = 10; y = 10;
	}
	
[Test]
	public static def foo : int()
	{
		return = 10;
	}
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        public void TestAttributeOnLanguageBlock()
        {
            string src = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Imperative, version=""###"", Visibility(11), fingerprint=""FS54"", Test] 
{
	a = 19;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        public void TestAttributeWithLanguageBlockAndArrayExpression()
        {
            string src = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
def foo : int[]..[](p : var[]..[])
{
	a = { 1, { 2, 3 }, 4 };
	return = a[1];
}
[Associative, version=""###"", Visibility(11), fingerprint=""FS54"", Test] 
{
	a = {1, 2, 3};
	b = a[1];
	c = a[0];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        public void TestBasicArrayMethods()
        {
            string src = @"a = { 1, 2, { 3, 4, 5, { 6, 7, { 8, 9, { { 11 } } } } }, { 12, 13 } };
c = Count(a);
r = Rank(a);
a2 = Flatten(a);";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("r").Payload == 6);
            mirror.CompareArrays("a2", new List<Object> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, typeof(Int64));
        }

        [Test]
        public void TestStringConcatenation01()
        {
            string src = @"s1='a';
s2=""bcd"";
s3=s1+s2;

s4=""abc"";
s5='d';
s6=s4+s5;

s7=""ab"";
s8=""cd"";
s9=s7+s8;";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("s3", "abcd");
            thisTest.Verify("s6", "abcd");
            thisTest.Verify("s9", "abcd");
        }

        [Test]
        public void TestStringOperations()
        {
            string src = @"class A{}
s = ""ab"";
r1 = s + 3;
r2 = s + false;
r3 = s + null;
r4 = !s;
r44 = !A.A();//false
r444 = !1;
r5 = s == ""ab"";
r6 = s == s;
r7 = ""ab"" == ""ab"";
ns = s;
ns[0] = 1;
r8 = ns == {1, 'b'};
//r9 = "" == "";
//r10 = ("" == null);
r9 = s != ""ab"";
ss = ""abc"";
ss[0] = 'x';
m = ss;
r10 = """" == null;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.SetErrorMessage("1467274 - Sprint26: rev3611: type conversion checking through two paths");
            thisTest.Verify("r1", "ab3");
            thisTest.Verify("r2", "abfalse");
            thisTest.Verify("r3", null);
            thisTest.Verify("r4", false);
            thisTest.Verify("r44", false);
            thisTest.Verify("r444", false);
            thisTest.Verify("r5", true);
            thisTest.Verify("r6", true);
            thisTest.Verify("r7", true);
            thisTest.Verify("r8", true);
            thisTest.Verify("r9", false);
            thisTest.Verify("ss", "xbc");
            thisTest.Verify("r10", null);
        }

        [Test]
        public void TestStringTypeConversion()
        {
            //Assert.Fail("DNL-1467239 Sprint 26 - Rev 3425 type conversion - string to bool conversion failing");
            string src = @"def foo:bool(x:bool)
{
    return=x;
}
r1 = foo('h');
r2 = 'h' && true;
r3 = 'h' + 1;";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("r1", true);
            thisTest.Verify("r2", true);
            thisTest.Verify("r3", "h1");
        }

        [Test]
        public void TestTypeArrayAssign()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t", new Object[] { 1, 2, 3 });
        }

        [Test]
        public void TestTypeArrayAssign2()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t", new Object[] { 1, 2, 3 });
        }

        [Test]
        public void TestTypeArrayAssign3()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("b", new Object[] { 1, 2, 3 });
            thisTest.Verify("ret", new Object[] { 1, 2, 3 });
        }

        [Test]
        public void TestTypedAssignment01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", 5);
        }

        [Test]
        public void TestTypedAssignment02()
        {
            string code =
@" t1:int = 1;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t1", 4);
            thisTest.Verify("t2", 4.3);
            thisTest.Verify("t3", 4.9);
            thisTest.Verify("t4", 6.1);
        }

        [Test]
        public void TestTypedAssignment03()
        {
            string code =
@" 
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t1", 4);
            thisTest.Verify("t2", 4.3);
            thisTest.Verify("t3", 4.9);
            thisTest.Verify("t4", 6.1);
        }

        [Test]
        public void TestTypedAssignment04()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r1", null);
        }

        [Test]
        public void TestTypedAssignment05()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", null);
        }

        [Test]
        public void TestTypedAssignment06()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", true);
            thisTest.Verify("y", false);
        }

        [Test]
        [Category("Escalate")]
        [Category("ToFixJun")]
        public void TestPropAssignWithReplication()
        {
            //Assert.Fail("DNL-1467241 Sprint25: rev 3420 : Property assignments using replication is not working");
            string code =
@"class A
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("testx", new Object[] { 1, 2 });
            thisTest.Verify("test", new Object[] { 5, 5 });
        }

        [Test]
        public void TestPropAssignWithReplication02()
        {
            string code =
@"class A 
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("t", new Object[] { 5, 5 });
        }

        [Test]
        public void TestGlobalFunctionRecursion100()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 5050);
        }

        [Test]
        public void TestGlobalFunctionRecursion100_GlobalIncrement()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 5050);
            thisTest.Verify("z", 100);
        }

        [Test]
        public void TestGlobalFunctionRecursion100_GlobalIncrementInFunction01()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 5050);
            thisTest.Verify("z", 100);
        }

        [Test]
        public void TestGlobalFunctionRecursion100_GlobalIncrementInFunction02()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", 5050);
            thisTest.Verify("z", 200);
        }

        [Test]
        public void TestGlobalFunctionRecursionReplication()
        {
            string code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("y", new Object[] { 5050, 20100, 45150 });
        }

        [Test]
        public void TestContextInject01()
        {
            ProtoScript.Runners.ProtoRunner runner = new ProtoScript.Runners.ProtoRunner();
            string code =
@"
            // TODO Jun: Move this test to the existing location context inject testcases
            // Add state verification
            Dictionary<string, Object> context = new Dictionary<string, object>();
            context.Add("x", 1);
            context.Add("y", 2);
            ProtoScript.Runners.ProtoRunner.ProtoVMState vmstate = runner.PreStart(code, context);
            runner.RunToNextBreakpoint(vmstate);
        }

        [Test]
        public void TestBasicFFIReplicate()
        {
            string code =
@"
            code = string.Format("{0}\n{1}", "import(\"Math.dll\");", code);
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", new Object[] { 5.0, 6.0, 7.0 });
        }


        [Test]
        public void Test_Compare_Node_01()
        {
            string s1 = "a = 1;";
            string s2 = "a=(1);";

            ProtoCore.AST.AssociativeAST.CodeBlockNode commentNode = null;
            ProtoCore.AST.Node s1Root = GraphToDSCompiler.GraphUtilities.Parse(s1, out commentNode);
            ProtoCore.AST.Node s2Root = GraphToDSCompiler.GraphUtilities.Parse(s2, out commentNode);
            bool areEqual = s1Root.Equals(s2Root);
            Assert.AreEqual(areEqual, true);
        }

        [Test]
        public void Test_Compare_Node_02()
        {
            string s1 = "a = 1; b=2;";
            string s2 = "a=(1) ; b = (2);";
            ProtoCore.AST.AssociativeAST.CodeBlockNode commentNode = null;
            ProtoCore.AST.Node s1Root = GraphToDSCompiler.GraphUtilities.Parse(s1, out commentNode);
            ProtoCore.AST.Node s2Root = GraphToDSCompiler.GraphUtilities.Parse(s2, out commentNode);
            bool areEqual = s1Root.Equals(s2Root);
            Assert.AreEqual(areEqual, true);
        }

        [Test]
        public void Test_Compare_Node_03()
        {
            string s1 = "a     =   1;  c = a+1;";
            string s2 = "a = 1; c=a +    1;";
            ProtoCore.AST.AssociativeAST.CodeBlockNode commentNode = null;
            ProtoCore.AST.Node s1Root = GraphToDSCompiler.GraphUtilities.Parse(s1, out commentNode);
            ProtoCore.AST.Node s2Root = GraphToDSCompiler.GraphUtilities.Parse(s2, out commentNode);
            bool areEqual = s1Root.Equals(s2Root);
            Assert.AreEqual(areEqual, true);
        }
    }
}