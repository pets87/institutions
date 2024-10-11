using InstitutionsService.Models;

namespace InstitutionsService.Data
{
    public static class DataSeed
    {
        public static void Run(ApplicationDbContext context)
        {
            if (!context.Addresses.Any()) 
            {
                var addresses = new List<Address>() 
                {
                    new Address()
                    {
                        Id = 1,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Tammsaare tee",
                        House = "56",
                        PostalCode = "11316",
                        AddressText = "Harjumaa, Tallinna, Tammsaare tee 56, 11316"
                    },
                    new Address()
                    {
                        Id = 2,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Mustamäe tee",
                        Apartment = null,
                        House = "3",
                        PostalCode = "15033",
                        AddressText = "Harjumaa, Tallinn, Mustamäe tee 3, 15033"
                    },
                    new Address()
                    {
                        Id = 3,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Lehola",
                        Apartment = null,
                        House = "20",
                        PostalCode = "11620",
                        AddressText = "Harjumaa, Tallinn, Lehola 20, 11620"
                    },
                    new Address()
                    {
                        Id = 4,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Saue",
                        Street = "Kütise",
                        Apartment = null,
                        House = "8",
                        PostalCode = "76505",
                        AddressText = "Harjumaa, Tallinn, Kütise 8, 76505"
                    },
                    new Address()
                    {
                        Id = 5,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Mäealuse",
                        Apartment = "500",
                        House = "2/2",
                        PostalCode = "12618",
                        AddressText = "Harjumaa, Tallinn, Mäealuse 2/2 - 500, 12618"
                    },
                    new Address()
                    {
                        Id = 6,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Pae",
                        Apartment = null,
                        House = "12",
                        PostalCode = "13620",
                        AddressText = "Harjumaa, Tallinn, Pae 12, 13620"
                    },
                    new Address()
                    {
                        Id = 7,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Kose",
                        Apartment = null,
                        House = "5",
                        PostalCode = "13813",
                        AddressText = "Harjumaa, Tallinn, Kose 5, 13813"
                    },
                    new Address()
                    {
                        Id = 8,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Viimsi",
                        Apartment = "1",
                        House = "34",
                        PostalCode = "74001",
                        AddressText = "Harjumaa, Tallinn, Viimsi 34 - 1, 74001"
                    },
                    new Address()
                    {
                        Id = 9,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Järve",
                        Apartment = null,
                        House = "10",
                        PostalCode = "11314",
                        AddressText = "Harjumaa, Tallinn, Järve 10, 11314"
                    },
                    new Address()
                    {
                        Id = 10,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Suur-Äkvere",
                        Apartment = "2",
                        House = "8",
                        PostalCode = "11712",
                        AddressText = "Harjumaa, Tallinn, Suur-Äkvere 8 - 2, 11712"
                    },
                    new Address()
                    {
                        Id = 11,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Kalevi",
                        Apartment = null,
                        House = "21",
                        PostalCode = "12116",
                        AddressText = "Harjumaa, Tallinn, Kalevi 21, 12116"
                    },
                    new Address()
                    {
                        Id = 12,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Saue",
                        Street = "Tamme",
                        Apartment = null,
                        House = "9",
                        PostalCode = "76506",
                        AddressText = "Harjumaa, Saue, Tamme 9, 76506"
                    },
                    new Address()
                    {
                        Id = 13,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Küti",
                        Apartment = "4",
                        House = "18",
                        PostalCode = "12113",
                        AddressText = "Harjumaa, Tallinn, Küti 18 - 4, 12113"
                    },
                    new Address()
                    {
                        Id = 14,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Mere",
                        Apartment = null,
                        House = "17",
                        PostalCode = "12117",
                        AddressText = "Harjumaa, Tallinn, Mere 17, 12117"
                    },
                    new Address()
                    {
                        Id = 15,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Pikk",
                        Apartment = "3",
                        House = "45",
                        PostalCode = "10133",
                        AddressText = "Harjumaa, Tallinn, Pikk 45 - 3, 10133"
                    },
                    new Address()
                    {
                        Id = 16,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Roheline",
                        Apartment = null,
                        House = "14",
                        PostalCode = "10121",
                        AddressText = "Harjumaa, Tallinn, Roheline 14, 10121"
                    },
                    new Address()
                    {
                        Id = 17,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Päev",
                        Apartment = null,
                        House = "27",
                        PostalCode = "10612",
                        AddressText = "Harjumaa, Tallinn, Päev 27, 10612"
                    },
                    new Address()
                    {
                        Id = 18,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Koidu",
                        Apartment = "7",
                        House = "60",
                        PostalCode = "12123",
                        AddressText = "Harjumaa, Tallinn, Koidu 60 - 7, 12123"
                    },
                    new Address()
                    {
                        Id = 19,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Kase",
                        Apartment = null,
                        House = "1",
                        PostalCode = "12621",
                        AddressText = "Harjumaa, Tallinn, Kase 1, 12621"
                    },
                    new Address()
                    {
                        Id = 20,
                        Country = "Eesti",
                        County = "Harjumaa",
                        City = "Tallinn",
                        Street = "Rästa",
                        Apartment = "8",
                        House = "39",
                        PostalCode = "11412",
                        AddressText = "Harjumaa, Tallinn, Rästa 39 - 8, 11412"
                    }
                };
                context.AddRange(addresses);
                context.SaveChanges();
            }
            if (!context.Persons.Any())
            {
                var persons = new List<Person>
                {
                    new Person
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Anderson",
                        PersonCode = "38605043778",
                        BirthDate = new DateTime(1986, 05, 04, 12, 23, 21, DateTimeKind.Utc)
                    },
                    new Person
                    {
                        Id = 2,
                        FirstName = "Roberto",
                        LastName = "Dinamite",
                        PersonCode = "46806164715",
                        BirthDate = new DateTime(1968, 06, 16, 10, 12, 52, DateTimeKind.Utc)
                    },
                    new Person
                    {
                        Id = 3,
                        FirstName = "Marek",
                        LastName = "Plura",
                        PersonCode = "37412180181",
                        BirthDate = new DateTime(1974, 12, 18, 18, 52, 13, DateTimeKind.Utc)
                    }
                };
                context.AddRange(persons);
                context.SaveChanges();
            }
            if (!context.Classifiers.Any())
            {
                var classifiers = new List<Classifier>
                {
                    new Classifier
                    {
                        Id = 1,
                        Group = "INSTITUTION_TYPE",
                        Name = "Osaühing",
                        NameTranslationCode = "classifier.1.name"
                    },
                    new Classifier
                    {
                        Id = 2,
                        Group = "INSTITUTION_TYPE",
                        Name = "Aktsiaselts",
                        NameTranslationCode = "classifier.2.name"
                    },
                    new Classifier
                    {
                        Id = 3,
                        Group = "INSTITUTION_TYPE",
                        Name = "Riigiasutus",
                        NameTranslationCode = "classifier.3.name"
                    },
                    new Classifier
                    {
                        Id = 4,
                        Group = "REPLICAITON_ENV",
                        Name = "DEV",
                        NameTranslationCode = "classifier.4.name"
                    },
                    new Classifier
                    {
                        Id = 5,
                        Group = "REPLICAITON_ENV",
                        Name = "TEST",
                        NameTranslationCode = "classifier.5.name"
                    },
                    new Classifier
                    {
                        Id = 6,
                        Group = "REPLICAITON_ENV",
                        Name = "PRELIVE",
                        NameTranslationCode = "classifier.6.name"
                    },
                    new Classifier
                    {
                        Id = 7,
                        Group = "REPLICAITON_ENV",
                        Name = "LIVE",
                        NameTranslationCode = "classifier.7.name"
                    },
                    new Classifier
                    {
                        Id = 8,
                        Group = "REPLICAITON_SYSTEM",
                        Name = "Kinnisvara portaal",
                        NameTranslationCode = "classifier.8.name"
                    },
                    new Classifier
                    {
                        Id = 9,
                        Group = "REPLICAITON_SYSTEM",
                        Name = "Auto portaal",
                        NameTranslationCode = "classifier.9.name"
                    },
                    new Classifier
                    {
                        Id = 10,
                        Group = "REPLICAITON_SYSTEM",
                        Name = "Tööportaal",
                        NameTranslationCode = "classifier.10.name"
                    },
                    new Classifier
                    {
                        Id = 11,
                        Group = "REPLICAITON_SYSTEM",
                        Name = "Epood",
                        NameTranslationCode = "classifier.11.name"
                    }
                };
                context.AddRange(classifiers);
                context.SaveChanges();
            }
            if (!context.Institutions.Any())
            {
                var institutions = new List<Institution>() {
                    new Institution{
                        Id = 1,
                        Name = "Srini OÜ",
                        NameTranslationCode = null,
                        RegCode = "14449790",
                        KMKR = "EE102059250",
                        AddressId = 1,
                        TypeClassifierId = 1,
                        ValidFrom = DateTime.UtcNow.AddYears(2)
                    },
                     new Institution{
                        Id = 2,
                        Name = "Pagari tänav OÜ",
                        NameTranslationCode = null,
                        RegCode = "14270849",
                        KMKR = "EE102002058",
                        AddressId = 3,
                        TypeClassifierId = 1,
                        ValidFrom = DateTime.UtcNow.AddYears(2),
                        ValidTo = DateTime.UtcNow
                    },
                     new Institution{
                        Id = 3,
                        Name = "Telia eesti AS",
                        NameTranslationCode = null,
                        RegCode = "10234957",
                        KMKR = "EE100070008",
                        AddressId = 2,
                        TypeClassifierId = 2,
                        ValidFrom = DateTime.UtcNow.AddYears(2)
                    },
                     new Institution{
                        Id = 4,
                        Name = "Saue vallavalitsus",
                        NameTranslationCode = $"institution.{4}.name",
                        RegCode = "77000430",
                        KMKR = "EE102061251",
                        AddressId = 4,
                        TypeClassifierId = 3,
                        ValidFrom = DateTime.UtcNow.AddYears(2)
                    },
                     new Institution{
                        Id =5,
                        Name = "Siseministeeriumi arenduskeskus",
                        NameTranslationCode = $"institution.{5}.name",
                        RegCode = "70008440",
                        KMKR = "EE101316392",
                        AddressId = 5,
                        TypeClassifierId = 3,
                        ValidFrom = DateTime.UtcNow.AddYears(2)
                    }
                };
                context.AddRange(institutions);
                context.SaveChanges();
            }
            if (!context.InstitutionContacts.Any())
            {
                var institutionContacts = new List<InstitutionContact>() {
                    new InstitutionContact(){
                        Id = 1,
                        InstitutionId = 1,
                        PersonId = 1,
                        Telephone = "+372555 55 555",
                        Email = "info@institution1.com"
                    },
                    new InstitutionContact(){
                        Id = 2,
                        InstitutionId = 2,
                        PersonId = 2,
                        Telephone = "+372555 55 555",
                        Email = "info@institution2.com"
                    },
                    new InstitutionContact(){
                        Id = 3,
                        InstitutionId = 3,
                        PersonId = 3,
                        Telephone = "+372555 55 555",
                        Email = "info@institution3.com"
                    },
                    new InstitutionContact(){
                        Id = 4,
                        InstitutionId = 4,
                        PersonId = 1,
                        Telephone = "+372555 55 555",
                        Email = "info@institution4.com"
                    },
                    new InstitutionContact(){
                        Id = 5,
                        InstitutionId = 5,
                        PersonId = 2,
                        Telephone = "+372555 55 555",
                        Email = "info@institution5.com"
                    }
                };
                context.AddRange(institutionContacts);
                context.SaveChanges();
            }
            if (!context.Translations.Any())
            {
                var translations = new List<Translation>
                {
                    new Translation { Id = 1, Type = "FRONTEND", Language = "en", Code = "host.app.nav.institutions", Text = "Institutions" },
                    new Translation { Id = 2, Type = "FRONTEND", Language = "en", Code = "host.app.footer.email", Text = "abi@rahvastikuregister.ee" },
                    new Translation { Id = 3, Type = "FRONTEND", Language = "en", Code = "host.app.footer.rrinfo", Text = "Read about population register" },
                    new Translation { Id = 4, Type = "FRONTEND", Language = "en", Code = "host.app.footer.govinfo", Text = "Data of local governments" },
                    new Translation { Id = 5, Type = "FRONTEND", Language = "en", Code = "host.app.footer.terms", Text = "Terms of usage" },
                    new Translation { Id = 6, Type = "FRONTEND", Language = "en", Code = "host.page-admin.description", Text = "This admin page has been created as a demonstration. Select 'Institutions' from the menu to see the sample work. The application has been built using the 'module federation' approach, as the tender specified that a microfrontend approach is being used." },
                    new Translation { Id = 7, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.type", Text = "Type" },
                    new Translation { Id = 8, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.name", Text = "Name" },
                    new Translation { Id = 9, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.regcode", Text = "Reg Code" },
                    new Translation { Id = 10, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.kmkr", Text = "KMKR" },
                    new Translation { Id = 11, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.address", Text = "Address" },
                    new Translation { Id = 12, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.validfrom", Text = "Valid From" },
                    new Translation { Id = 13, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.validto", Text = "Valid To" },
                    new Translation { Id = 14, Type = "FRONTEND", Language = "et", Code = "host.app.footer.email", Text = "abi@rahvastikuregister.ee" },
                    new Translation { Id = 15, Type = "FRONTEND", Language = "et", Code = "host.app.footer.govinfo", Text = "Kohalike omavalitsuste andmed" },
                    new Translation { Id = 16, Type = "FRONTEND", Language = "et", Code = "host.app.footer.terms", Text = "Kasutustingimused" },
                    new Translation { Id = 17, Type = "FRONTEND", Language = "et", Code = "host.page-admin.description", Text = "See admin leht on loodud ainult näidisena. Vali menüüst 'Asutused', et näha proovitööd. Rakendus on üles ehitatud kasutades 'module federation' lähenemist, kuna hankes oli välja toodud, et kasutusel on mikrofrontend lähenemine." },
                    new Translation { Id = 18, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.type", Text = "Tüüp" },
                    new Translation { Id = 19, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.name", Text = "Nimi" },
                    new Translation { Id = 20, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.regcode", Text = "Reg Kood" },
                    new Translation { Id = 21, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.kmkr", Text = "KMKR" },
                    new Translation { Id = 22, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.address", Text = "Aadress" },
                    new Translation { Id = 23, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.validfrom", Text = "Kehtiv alates" },
                    new Translation { Id = 24, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.validto", Text = "Kehtiv Kuni" },
                    new Translation { Id = 25, Type = "INSTITUTION", Language = "et", Code = "institution.4.name", Text = "Saue vallavalitsus" },
                    new Translation { Id = 26, Type = "INSTITUTION", Language = "en", Code = "institution.4.name", Text = "Saue municipal government" },
                    new Translation { Id = 27, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.table.col.actions", Text = "The actions" },
                    new Translation { Id = 28, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.table.col.actions", Text = "Tegevused" },
                    new Translation { Id = 29, Type = "FRONTEND", Language = "et", Code = "form.button.back", Text = "Tagasi" },
                    new Translation { Id = 30, Type = "FRONTEND", Language = "en", Code = "form.button.back", Text = "Back" },
                    new Translation { Id = 31, Type = "FRONTEND", Language = "et", Code = "form.button.save", Text = "Salvesta" },
                    new Translation { Id = 32, Type = "FRONTEND", Language = "en", Code = "form.button.save", Text = "Save" },
                    new Translation { Id = 33, Type = "CLASSIFIER", Language = "et", Code = "classifier.1.name", Text = "Osaühing" },
                    new Translation { Id = 34, Type = "CLASSIFIER", Language = "en", Code = "classifier.1.name", Text = "Limited company" },
                    new Translation { Id = 35, Type = "CLASSIFIER", Language = "et", Code = "classifier.2.name", Text = "Aktsiaselts" },
                    new Translation { Id = 36, Type = "CLASSIFIER", Language = "en", Code = "classifier.2.name", Text = "Stock company" },
                    new Translation { Id = 37, Type = "CLASSIFIER", Language = "et", Code = "classifier.3.name", Text = "Riigiasutus" },
                    new Translation { Id = 38, Type = "CLASSIFIER", Language = "en", Code = "classifier.3.name", Text = "State institution" },
                    new Translation { Id = 39, Type = "FRONTEND", Language = "et", Code = "form.label.translations", Text = "Tõlked" },
                    new Translation { Id = 40, Type = "FRONTEND", Language = "en", Code = "form.label.translations", Text = "Translations" },
                    new Translation { Id = 41, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.translations.description", Text = "Riigiasutused on tõlgitavad" },
                    new Translation { Id = 42, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.translations.description", Text = "State institutions are translatable" },
                    new Translation { Id = 43, Type = "FRONTEND", Language = "et", Code = "form.date.format", Text = "pp/kk/aaaa" },
                    new Translation { Id = 44, Type = "FRONTEND", Language = "en", Code = "form.date.format", Text = "mm/dd/yyyy" },
                    new Translation { Id = 45, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.reg_code", Text = "Registrikood on kohustuslik" },
                    new Translation { Id = 46, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.reg_code", Text = "RegCode is mandatory" },
                    new Translation { Id = 47, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.type_classifier_id", Text = "Tüüp on kohustuslik" },
                    new Translation { Id = 48, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.type_classifier_id", Text = "Type is mandatory" },
                    new Translation { Id = 49, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.address_id", Text = "Aadress on kohustuslik" },
                    new Translation { Id = 50, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.address_id", Text = "Address is mandatory" },
                    new Translation { Id = 51, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.valid_from", Text = "Kehtiv alates on kohustuslik" },
                    new Translation { Id = 52, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.valid_from", Text = "Valid from is mandatory" },
                    new Translation { Id = 53, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.name", Text = "Nimi on kohustuslik" },
                    new Translation { Id = 54, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.name", Text = "Name is mandatory" },
                    new Translation { Id = 55, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.requiredfields", Text = "Täida kohustuslikud väljad" },
                    new Translation { Id = 56, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.requiredfields", Text = "Fill required fields" },
                    new Translation { Id = 57, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.notfound", Text = "Asutust ei leitud" },
                    new Translation { Id = 58, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.notfound", Text = "Institution not found" },
                    new Translation { Id = 59, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.saved", Text = "Asutust salvestatud" },
                    new Translation { Id = 60, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.saved", Text = "Institution saved" },
                    new Translation { Id = 61, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.publish", Text = "Publitseeri" },
                    new Translation { Id = 62, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.publish", Text = "Publish" },
                    new Translation { Id = 63, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.tab.institution", Text = "Asutus" },
                    new Translation { Id = 64, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.tab.institution", Text = "Institution" },
                    new Translation { Id = 65, Type = "CLASSIFIER", Language = "et", Code = "classifier.4.name", Text = "Arendus" },
                    new Translation { Id = 66, Type = "CLASSIFIER", Language = "en", Code = "classifier.4.name", Text = "Development" },
                    new Translation { Id = 67, Type = "CLASSIFIER", Language = "et", Code = "classifier.5.name", Text = "Test" },
                    new Translation { Id = 68, Type = "CLASSIFIER", Language = "en", Code = "classifier.5.name", Text = "Test" },
                    new Translation { Id = 69, Type = "CLASSIFIER", Language = "et", Code = "classifier.6.name", Text = "EelToodang" },
                    new Translation { Id = 70, Type = "CLASSIFIER", Language = "en", Code = "classifier.6.name", Text = "PreProduction" },
                    new Translation { Id = 71, Type = "CLASSIFIER", Language = "et", Code = "classifier.8.name", Text = "Kinnisvara portaal" },
                    new Translation { Id = 72, Type = "CLASSIFIER", Language = "en", Code = "classifier.8.name", Text = "Real estate portal" },
                    new Translation { Id = 73, Type = "CLASSIFIER", Language = "et", Code = "classifier.9.name", Text = "Auto portaal" },
                    new Translation { Id = 74, Type = "CLASSIFIER", Language = "en", Code = "classifier.9.name", Text = "Car portal" },
                    new Translation { Id = 75, Type = "CLASSIFIER", Language = "et", Code = "classifier.10.name", Text = "Tööportaal" },
                    new Translation { Id = 76, Type = "CLASSIFIER", Language = "en", Code = "classifier.10.name", Text = "Work portal" },
                    new Translation { Id = 77, Type = "CLASSIFIER", Language = "et", Code = "classifier.11.name", Text = "Epood" },
                    new Translation { Id = 78, Type = "CLASSIFIER", Language = "en", Code = "classifier.11.name", Text = "Store" },
                    new Translation { Id = 79, Type = "FRONTEND", Language = "et", Code = "form.label.system", Text = "Süsteem" },
                    new Translation { Id = 80, Type = "FRONTEND", Language = "en", Code = "form.label.system", Text = "System" },
                    new Translation { Id = 81, Type = "FRONTEND", Language = "et", Code = "form.button.plan", Text = "Planeeri" },
                    new Translation { Id = 82, Type = "FRONTEND", Language = "en", Code = "form.button.plan", Text = "Plan" },
                    new Translation { Id = 83, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.published", Text = "Asutused publitseeritud" },
                    new Translation { Id = 84, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.published", Text = "Institutions published" },
                    new Translation { Id = 85, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.planned", Text = "Asutuste publitseerimine planeeritud" },
                    new Translation { Id = 86, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.planned", Text = "Institutions publishing planned" },
                    new Translation { Id = 87, Type = "FRONTEND", Language = "et", Code = "form.label.published", Text = "Publitseeritud" },
                    new Translation { Id = 88, Type = "FRONTEND", Language = "en", Code = "form.label.published", Text = "Published" },
                    new Translation { Id = 89, Type = "FRONTEND", Language = "et", Code = "form.label.planned", Text = "Planeeritud" },
                    new Translation { Id = 90, Type = "FRONTEND", Language = "en", Code = "form.label.planned", Text = "Planned" },
                    new Translation { Id = 91, Type = "FRONTEND", Language = "et", Code = "form.label.plannedtime", Text = "Planeeritud aeg" },
                    new Translation { Id = 92, Type = "FRONTEND", Language = "en", Code = "form.label.plannedtime", Text = "Planned time" },
                    new Translation { Id = 93, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.planningtext", Text = "Vali aeg, millal planeerida valitud kirjete publitseerimine. Valitud kuupäeval publitseeritakse kirjed automaatselt. Planeeritud ja publitseeritud kirjeid ei ole võimalik muuta." },
                    new Translation { Id = 94, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.planningtext", Text = "Select the time to schedule the publication of the chosen entries. The entries will be published automatically on the selected date. Planned and published entries cannot be edited." },
                    new Translation { Id = 95, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.error.plannedtime", Text = "Planeeritud aeg on kohustuslik" },
                    new Translation { Id = 96, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.error.plannedtime", Text = "Planned time is mandatory" },
                    new Translation { Id = 97, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.button.newinstitution", Text = "Uus asutus" },
                    new Translation { Id = 98, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.button.newinstitution", Text = "New institution" },
                    new Translation { Id = 99, Type = "FRONTEND", Language = "et", Code = "form.confirm.delete", Text = "Kas oled kindel, et soovid kirje kustutada?" },
                    new Translation { Id = 100, Type = "FRONTEND", Language = "en", Code = "form.confirm.delete", Text = "Are you sure you want to delete this row?" },
                    new Translation { Id = 101, Type = "FRONTEND", Language = "et", Code = "institutions.page-main.button.massactions", Text = "Masstegevused" },
                    new Translation { Id = 102, Type = "FRONTEND", Language = "en", Code = "institutions.page-main.button.massactions", Text = "Mass actions" },
                    new Translation { Id = 103, Type = "FRONTEND", Language = "et", Code = "form.button.cancel", Text = "Tühista" },
                    new Translation { Id = 104, Type = "FRONTEND", Language = "en", Code = "form.button.cancel", Text = "Cancel" },
                    new Translation { Id = 105, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.selectedinstitutions", Text = "Valitud asutused" },
                    new Translation { Id = 106, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.selectedinstitutions", Text = "Selected institutions" },
                    new Translation { Id = 107, Type = "FRONTEND", Language = "et", Code = "form.label.environment", Text = "Keskkond" },
                    new Translation { Id = 108, Type = "FRONTEND", Language = "en", Code = "form.label.environment", Text = "Environment" },
                    new Translation { Id = 109, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.history", Text = "Publitseerimise Ajalugu" },
                    new Translation { Id = 110, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.history", Text = "Publish history" },
                    new Translation { Id = 111, Type = "FRONTEND", Language = "et", Code = "form.label.translationcode", Text = "Tõlke kood" },
                    new Translation { Id = 112, Type = "FRONTEND", Language = "en", Code = "form.label.translationcode", Text = "Translation code" },
                    new Translation { Id = 113, Type = "FRONTEND", Language = "et", Code = "host.app.footer.rrinfo", Text = "Loe rahvastikuregistri kohta" },
                    new Translation { Id = 114, Type = "FRONTEND", Language = "et", Code = "form.label.translationsaved", Text = "Tõlge salvestatud" },
                    new Translation { Id = 115, Type = "FRONTEND", Language = "en", Code = "form.label.translationsaved", Text = "Translation saved" },
                    new Translation { Id = 116, Type = "FRONTEND", Language = "et", Code = "component-edit-translations.changetext", Text = "Muutmisel toimub automaatne salvestamine" },
                    new Translation { Id = 117, Type = "FRONTEND", Language = "en", Code = "component-edit-translations.changetext", Text = "Changes are saved automatically." },
                    new Translation { Id = 118, Type = "FRONTEND", Language = "et", Code = "component-edit-translations.infotext", Text = "Uute tõlgete lisamist ei ole loodud, kuna uue tõlke kasutusele võtmine vajab ka koodimuudatust" },
                    new Translation { Id = 119, Type = "FRONTEND", Language = "en", Code = "component-edit-translations.infotext", Text = "Adding new translations has not been implemented because adopting a new translation also requires a code change." },
                    new Translation { Id = 120, Type = "FRONTEND", Language = "et", Code = "institutions.page-edit-institution.tab.publish", Text = "Publitseeri" },
                    new Translation { Id = 121, Type = "FRONTEND", Language = "en", Code = "institutions.page-edit-institution.tab.publish", Text = "Publish" },
                    new Translation { Id = 122, Type = "FRONTEND", Language = "et", Code = "host.app.nav.institutions", Text = "Asutused" },
                    new Translation { Id = 123, Type = "INSTITUTION", Language = "et", Code = "institution.5.name", Text = "Siseministeeriumi arenduskeskus" },
                    new Translation { Id = 124, Type = "INSTITUTION", Language = "en", Code = "institution.5.name", Text = "Interior Ministry Dev Center" },
                    new Translation { Id = 125, Type = "CLASSIFIER", Language = "et", Code = "classifier.7.name", Text = "Toodang" },
                    new Translation { Id = 126, Type = "CLASSIFIER", Language = "en", Code = "classifier.7.name", Text = "Production" },
                };

                context.Translations.AddRange(translations);
                context.SaveChanges();
            }
            if (!context.InstitutionReplications.Any()) 
            {
                var institutionReplications = new List<InstitutionReplication>() 
                {
                    new InstitutionReplication()
                    {
                        Id = 1,
                        InstitutionId = 1,
                        EnvironmentClassifierId = 4,
                        SystemClassifierId = 7,
                        PublishedDateTime = DateTime.Now.AddDays(-1),
                    },
                    new InstitutionReplication()
                    {
                        Id = 2,
                        InstitutionId = 1,
                        EnvironmentClassifierId = 5,
                        SystemClassifierId = 7,
                        PlannedPublishDateTime = DateTime.Now.AddDays(1),
                    }
                };

                context.AddRange(institutionReplications);
                context.SaveChanges();
            }
        }
    }
}
