﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Represents an OpenLibrary Edition request.
    /// </summary>
    public sealed record OLEditionData : OLContainer
    {
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        [JsonProperty("key")]
        public string Key { get; init; } = "";
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        [JsonProperty("description")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.DescriptionConverter))]
        public string Description { get; init; } = "";
        [JsonProperty("number_of_pages")]
        public int PageCount { get; init; } = -1;
        [JsonProperty("identifiers")]
        public OLEditionIdentifiers Identifiers { get; init; } = new OLEditionIdentifiers();

        [JsonIgnore]
        public IReadOnlyList<string> ISBN => new ReadOnlyCollection<string>(new List<string>(isbn10.Concat(isbn13)));

        [JsonIgnore]
        public IReadOnlyList<string> ISBN10
        {
            get => new ReadOnlyCollection<string>(isbn10);
            init => isbn10 = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> ISBN13
        {
            get => new ReadOnlyCollection<string>(isbn13);
            init => isbn13 = value.ToArray();
        }

        [JsonIgnore]
        public IReadOnlyList<string> AuthorKeys
        {
            get => new ReadOnlyCollection<string>(authors);
            init => authors = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<int> CoverKeys
        {
            get => new ReadOnlyCollection<int>(coverKeys);
            init => coverKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> WorkKeys
        {
            get => new ReadOnlyCollection<string>(workKeys);
            init => workKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(subjects);
            init => subjects = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> Publishers
        {
            get => new ReadOnlyCollection<string>(publishers);
            init => publishers = value.ToArray();
        }

        [JsonProperty("isbn_10")]
        private string[] isbn10 { get; init; } = new string[0];
        [JsonProperty("isbn_13")]
        private string[] isbn13 { get; init; } = new string[0];

        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionsAuthorsKeysConverter))]
        private string[] authors { get; init; } = new string[0];
        [JsonProperty("covers")]
        private int[] coverKeys { get; init; } = new int[0];
        [JsonProperty("works")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.WorksKeysConverter))]
        private string[] workKeys { get; init; } = new string[0];
        [JsonProperty("subjects")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionSubjectsConverter))]
        private string[] subjects { get; init; } = new string[0];
        [JsonProperty("publishers")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionPublishersConverter))]
        private string[] publishers { get; init; } = new string[0];

        /// <summary>
        /// Holds the various identifiers of an edition.
        /// </summary>
        public sealed record OLEditionIdentifiers : OLContainer
        {
            [JsonIgnore]
            public IReadOnlyList<string> Goodreads
            {
                get => new ReadOnlyCollection<string>(goodreads);
                init => goodreads = value.ToArray();
            }
            [JsonIgnore]
            public IReadOnlyList<string> LibraryThing
            {
                get => new ReadOnlyCollection<string>(libraryThing);
                init => libraryThing = value.ToArray();
            }

            [JsonProperty("goodreads")]
            private string[] goodreads { get; init; } = new string[0];
            [JsonProperty("librarything")]
            private string[] libraryThing { get; init; } = new string[0];

            public bool Equals(OLEditionIdentifiers? data)
            {
                return
                    data != null &&
                    CompareExtensionData(data.extensionData) &&
                    GeneralUtility.SequenceEqual(this.goodreads, data.goodreads) &&
                    GeneralUtility.SequenceEqual(this.libraryThing, data.libraryThing);
            }

            public override int GetHashCode() => base.GetHashCode();
        }

        public bool Equals(OLEditionData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Key == data.Key &&
                this.Title == data.Title &&
                this.Description == data.Description &&
                this.PageCount == data.PageCount &&
                this.Identifiers == data.Identifiers &&
                GeneralUtility.SequenceEqual(this.isbn10, data.isbn10) &&
                GeneralUtility.SequenceEqual(this.isbn13, data.isbn13) &&
                GeneralUtility.SequenceEqual(this.authors, data.authors) &&
                GeneralUtility.SequenceEqual(this.coverKeys, data.coverKeys) &&
                GeneralUtility.SequenceEqual(this.workKeys, data.workKeys) &&
                GeneralUtility.SequenceEqual(this.subjects, data.subjects) &&
                GeneralUtility.SequenceEqual(this.publishers, data.publishers);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
