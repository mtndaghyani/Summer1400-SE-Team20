using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Classes.Indexers;
using SearchEngine.Classes.IO.Database;
using SearchEngine.Classes.IO.Database.Models;
using Xunit;
using Xunit.Abstractions;

namespace SearchEngineTests.DatabaseTests
{
    public abstract class DatabaseInvertedIndexTests
    {
        protected DbContextOptions<IndexingContext> ContextOptions;
        // private readonly ITestOutputHelper _testOutputHelper;
        public DatabaseInvertedIndexTests(DbContextOptions<IndexingContext> contextOptions)
        {
            // _testOutputHelper = testOutputHelper;
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new IndexingContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var word1 = new Word() {Statement = "jafari"};
                var word2 = new Word() {Statement = "jahangir"};
                var word3 = new Word() {Statement = "gheisarieh"};
                var document1 = new Document() {DocumentNumber = 3};
                var document2 = new Document() {DocumentNumber = 5};
                var wordDoc1 = new WordDocument() {Document = document1, Word = word1};
                var wordDoc2 = new WordDocument() {Document = document1, Word = word2};
                var wordDoc3 = new WordDocument() {Document = document2, Word = word1};
                var wordDoc4 = new WordDocument() {Document = document2, Word = word3};
                
                context.Words.AddRange(word1, word2);
                context.Documents.AddRange(document1, document2);
                context.WordDocuments.AddRange(wordDoc1, wordDoc2, wordDoc3, wordDoc4);
                
                context.SaveChanges();
            }
        }
        
        [Fact]
        public void TestContainsKey_WHEN_jafari_EXPECTED_True()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            bool result = databaseInvertedIndex.ContainsKey("jafari");
            Assert.True(result);
        }
        
        [Fact]
        public void TestContainsKey_WHEN_motahari_EXPECTED_False()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            bool result = databaseInvertedIndex.ContainsKey("motahari");
            Assert.False(result);
        }
        
        [Fact]
        public void TestGet_WHEN_jafari_EXPECTED_3And5()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            HashSet<Document> result = databaseInvertedIndex.Get("jafari");
            Utils.AssertEqualDocumentEnumerable(new List<int>(new int[]{3, 5}), result);
        }
        
        [Fact]
        public void TestGet_WHEN_motahari_EXPECTED_empty()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            HashSet<Document> result = databaseInvertedIndex.Get("motahari");
            Utils.AssertEqualDocumentEnumerable(new List<int>(new int[]{}), result);
        }
        
        [Fact]
        public void TestGet_WHEN_gheisarieh_EXPECTED_5()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            HashSet<Document> result = databaseInvertedIndex.Get("gheisarieh");
            Utils.AssertEqualDocumentEnumerable(new List<int>(new int[]{5}), result);
        }
        
        [Fact]
        public void TestAdd_WHEN_AsgharNam_10_EXPECTED_Exist()
        {
            DatabaseInvertedIndex databaseInvertedIndex = new DatabaseInvertedIndex("InMemory");
            Document doc = new Document() {DocumentNumber = 10};
            string statement = "AsgharNam";
            databaseInvertedIndex.Add(statement, doc);
            using var context = new IndexingContext(ContextOptions);
            bool wordResult = context.Words.Any(word => word.Statement == statement);
            Assert.True(wordResult);
            bool docResult = context.Documents.Any(document => document.DocumentNumber == 10);
            Assert.True(docResult);
            bool wordDocResult = context.WordDocuments.Any(wordDoc =>
                Equals(wordDoc.Document.DocumentNumber, doc.DocumentNumber) &&
                Equals(wordDoc.Word.Statement, statement));
            Assert.True(wordDocResult);
        }
    }
}