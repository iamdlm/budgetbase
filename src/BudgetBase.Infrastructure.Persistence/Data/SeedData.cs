﻿using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Infrastructure.Persistence.Data
{
    public static class SeedData
    {
        public static readonly Bank[] Banks =
        {
            new() { Id = new Guid("4e9bd441-4a5a-4163-b02e-26dbe04b9052"), Name = "Millenium bcp", CountryId = new Guid("4d5875ff-211a-45fb-9772-a8a2f97f127c") },
            new() { Id = new Guid("bbcef358-83a6-4d63-92b8-6ad22c944a1b"), Name = "Moey", CountryId = new Guid("4d5875ff-211a-45fb-9772-a8a2f97f127c") },
            new() { Id = new Guid("f343d2a0-3c3a-4a9e-98a3-60afb3c09510"), Name = "ActivoBank", CountryId = new Guid("4d5875ff-211a-45fb-9772-a8a2f97f127c") }
        };

        public static readonly Country[] Countries =
        {
            new() { Id = new Guid("4662ac04-16b4-4252-a10b-b135a930b40e"), Name = "Afghanistan", Code = "AF" },
            new() { Id = new Guid("64a4f7aa-8552-4dd3-8555-e26dcce18aac"), Name = "Åland Islands", Code = "AX" },
            new() { Id = new Guid("8ff4f416-7e64-46d4-bbf5-af7704dbb798"), Name = "Albania", Code = "AL" },
            new() { Id = new Guid("cbadce0c-182c-4b86-93cf-479aa36b0c81"), Name = "Algeria", Code = "DZ" },
            new() { Id = new Guid("ec4aa6df-f02b-4182-897b-6e76c8d40e1f"), Name = "American Samoa", Code = "AS" },
            new() { Id = new Guid("9134b212-8818-446e-99fd-75770d2fa172"), Name = "Andorra", Code = "AD" },
            new() { Id = new Guid("31ab4b8b-a5de-4507-9c35-fad716785872"), Name = "Angola", Code = "AO" },
            new() { Id = new Guid("8bc0ff62-b94e-4838-8764-afc3e6234b3b"), Name = "Anguilla", Code = "AI" },
            new() { Id = new Guid("1694b1d9-3d01-422d-bb3e-df510babe130"), Name = "Antarctica", Code = "AQ" },
            new() { Id = new Guid("c126a25e-5c9c-4686-85a8-48ae7f4c8eb3"), Name = "Antigua and Barbuda", Code = "AG" },
            new() { Id = new Guid("15180284-f003-4703-bd81-de7166573dc9"), Name = "Argentina", Code = "AR" },
            new() { Id = new Guid("6e89e601-ff90-42a2-ba87-16dc44de0b8d"), Name = "Armenia", Code = "AM" },
            new() { Id = new Guid("9f0c69c8-4c07-42a7-97ce-7bfa1a5da983"), Name = "Aruba", Code = "AW" },
            new() { Id = new Guid("44212d52-4a01-4500-a8d9-fcad69f5be4a"), Name = "Australia", Code = "AU" },
            new() { Id = new Guid("5789ae52-9d06-4204-988a-a14c117d1b34"), Name = "Austria", Code = "AT" },
            new() { Id = new Guid("a613eb49-d1c1-4094-90b0-5a3e4d39b7ba"), Name = "Azerbaijan", Code = "AZ" },
            new() { Id = new Guid("7aeeddd1-91fd-4e24-9b64-7e4a2c261979"), Name = "Bahamas", Code = "BS" },
            new() { Id = new Guid("76c6888e-0d70-4e44-bcf3-d40792f8a352"), Name = "Bahrain", Code = "BH" },
            new() { Id = new Guid("452d3cbe-547f-4df3-adf9-f901ddec7cad"), Name = "Bangladesh", Code = "BD" },
            new() { Id = new Guid("214d8594-1b15-4682-9fde-54f6ebcc166e"), Name = "Barbados", Code = "BB" },
            new() { Id = new Guid("2ec68dae-0960-44ba-9dfc-b545487d74ae"), Name = "Belarus", Code = "BY" },
            new() { Id = new Guid("8ba803d5-c948-4d11-9783-4735ad5fb55b"), Name = "Belgium", Code = "BE" },
            new() { Id = new Guid("cc606365-0076-4f79-a2f2-cb91287a9694"), Name = "Belize", Code = "BZ" },
            new() { Id = new Guid("568eca12-038b-4468-baff-2dc32ce7596b"), Name = "Benin", Code = "BJ" },
            new() { Id = new Guid("43224527-94e6-46f8-bf5e-15eccf1a2b08"), Name = "Bermuda", Code = "BM" },
            new() { Id = new Guid("17c27ae1-d7cc-4108-ae4e-b749baf8faa6"), Name = "Bhutan", Code = "BT" },
            new() { Id = new Guid("d6b3380a-0f8d-45fe-b46c-7fd97b1a2595"), Name = "Bolivia, Plurinational State of", Code = "BO" },
            new() { Id = new Guid("a4a582f4-3fc7-4967-a2fa-bceea950d095"), Name = "Bonaire, Sint Eustatius and Saba", Code = "BQ" },
            new() { Id = new Guid("b2366c9b-5ccd-473b-81e9-095feca6efc8"), Name = "Bosnia and Herzegovina", Code = "BA" },
            new() { Id = new Guid("799027d0-14b8-449c-8f1c-c905c5e10c96"), Name = "Botswana", Code = "BW" },
            new() { Id = new Guid("6b67ed57-d276-4bd2-b2a1-043190016234"), Name = "Bouvet Island", Code = "BV" },
            new() { Id = new Guid("62cfda9f-43c5-4c63-9c3b-1e9781a7d4f6"), Name = "Brazil", Code = "BR" },
            new() { Id = new Guid("16394164-bdcb-4993-84ac-30966a08a954"), Name = "British Indian Ocean Territory", Code = "IO" },
            new() { Id = new Guid("164b2512-a39d-4e94-a4cd-e21401fb8483"), Name = "Brunei Darussalam", Code = "BN" },
            new() { Id = new Guid("0d3d2937-f0cc-4849-a282-81f2d6b4080a"), Name = "Bulgaria", Code = "BG" },
            new() { Id = new Guid("8f979272-33eb-4d04-9fe1-7e9531d6761d"), Name = "Burkina Faso", Code = "BF" },
            new() { Id = new Guid("1792a9f1-ec2c-4851-970b-a05cfe84a279"), Name = "Burundi", Code = "BI" },
            new() { Id = new Guid("4449152d-37d1-4b34-bd64-16bcd279a376"), Name = "Cambodia", Code = "KH" },
            new() { Id = new Guid("b3c96583-80dd-42b5-903f-bcdf15308f56"), Name = "Cameroon", Code = "CM" },
            new() { Id = new Guid("e128b1e6-3f85-4ea7-be06-266a2e8cb2b0"), Name = "Canada", Code = "CA" },
            new() { Id = new Guid("e5838809-7285-4463-97af-711e42692cd2"), Name = "Cape Verde", Code = "CV" },
            new() { Id = new Guid("d9622068-8a1d-42c5-9305-359c49174afc"), Name = "Cayman Islands", Code = "KY" },
            new() { Id = new Guid("bcc74cc1-65af-4e18-9475-e117f164e3d6"), Name = "Central African Republic", Code = "CF" },
            new() { Id = new Guid("aeaef539-0f15-4e2f-b834-a2ad16d085fe"), Name = "Chad", Code = "TD" },
            new() { Id = new Guid("b31e8b47-5a56-461c-8fd6-559f05c50a79"), Name = "Chile", Code = "CL" },
            new() { Id = new Guid("9826bf90-29bc-463a-9632-c69b6cb7b432"), Name = "China", Code = "CN" },
            new() { Id = new Guid("d28eb78a-d157-4147-8232-23e1a3a66c66"), Name = "Christmas Island", Code = "CX" },
            new() { Id = new Guid("699e3f12-6206-4f65-b7c1-aeace357597f"), Name = "Cocos (Keeling) Islands", Code = "CC" },
            new() { Id = new Guid("c483e6cc-ca59-4b72-a934-46d914b631ca"), Name = "Colombia", Code = "CO" },
            new() { Id = new Guid("10ba9d84-0200-4ad0-93c9-b8f2d28aa3a2"), Name = "Comoros", Code = "KM" },
            new() { Id = new Guid("b6723fc3-8501-4de5-8fec-3d18b0247765"), Name = "Congo", Code = "CG" },
            new() { Id = new Guid("0d7a8772-d9ea-4d3d-8d3f-39cf1340fbe3"), Name = "Congo, the Democratic Republic of the", Code = "CD" },
            new() { Id = new Guid("862fe0a1-b615-41e4-b575-f9cc9e8f24e2"), Name = "Cook Islands", Code = "CK" },
            new() { Id = new Guid("893eb160-f2de-499c-aae6-7772b08e6732"), Name = "Costa Rica", Code = "CR" },
            new() { Id = new Guid("46031467-b519-4d57-867a-2a886028e13a"), Name = "Côte d'Ivoire", Code = "CI" },
            new() { Id = new Guid("152719d0-4b7d-4450-a947-28947a7e774f"), Name = "Croatia", Code = "HR" },
            new() { Id = new Guid("37541a1a-0633-47d3-ad33-4fe8bc1a9175"), Name = "Cuba", Code = "CU" },
            new() { Id = new Guid("dfb8aab9-e66a-4d7b-be00-e3318450fc99"), Name = "Curaçao", Code = "CW" },
            new() { Id = new Guid("19affc35-b69d-4cc1-807e-9e0e606d32dd"), Name = "Cyprus", Code = "CY" },
            new() { Id = new Guid("0b863526-2c1a-4966-87ff-eb98b6fe6f52"), Name = "Czech Republic", Code = "CZ" },
            new() { Id = new Guid("27452a02-b062-4917-8b7a-179d953d1950"), Name = "Denmark", Code = "DK" },
            new() { Id = new Guid("a3907621-062e-4bc7-8dd4-d28879ab19ca"), Name = "Djibouti", Code = "DJ" },
            new() { Id = new Guid("8f2ab555-8fac-4db2-b908-b43729c5e163"), Name = "Dominica", Code = "DM" },
            new() { Id = new Guid("3d67c026-d9ca-46b7-bc09-a9ff58855b7e"), Name = "Dominican Republic", Code = "DO" },
            new() { Id = new Guid("ad7a3a90-928b-4364-bd3b-4ed0a4bc9fd3"), Name = "Ecuador", Code = "EC" },
            new() { Id = new Guid("0a34d89a-d112-44cd-9b2c-939032178710"), Name = "Egypt", Code = "EG" },
            new() { Id = new Guid("9e6fe767-3584-4aa8-9215-ea531fb09eb4"), Name = "El Salvador", Code = "SV" },
            new() { Id = new Guid("b36df8bb-ff5f-4b3b-86b6-28b864389681"), Name = "Equatorial Guinea", Code = "GQ" },
            new() { Id = new Guid("7bf4febc-06bf-4aba-9623-b3a3acc08ac2"), Name = "Eritrea", Code = "ER" },
            new() { Id = new Guid("8e079b3c-e304-4a1f-b734-1c06e5543a09"), Name = "Estonia", Code = "EE" },
            new() { Id = new Guid("935cd7c2-2626-435f-a7f9-a817e4088174"), Name = "Ethiopia", Code = "ET" },
            new() { Id = new Guid("4e9c9332-9efd-416a-a9aa-85a5a7565a90"), Name = "Falkland Islands (Malvinas)", Code = "FK" },
            new() { Id = new Guid("7c098329-f0c1-475e-bdfd-eb115bde5293"), Name = "Faroe Islands", Code = "FO" },
            new() { Id = new Guid("9968bdeb-1300-40b0-b7c9-742d93a5be77"), Name = "Fiji", Code = "FJ" },
            new() { Id = new Guid("231752b3-6561-4daf-8b42-7b1458b79cb5"), Name = "Finland", Code = "FI" },
            new() { Id = new Guid("e569e3ca-c59a-42ec-9fc0-9d3699de5a60"), Name = "France", Code = "FR" },
            new() { Id = new Guid("b2e811c9-2d3a-4889-80ab-4a05e0e730a8"), Name = "French Guiana", Code = "GF" },
            new() { Id = new Guid("eb616a98-ae41-4cc6-bc0e-7c7e001d2697"), Name = "French Polynesia", Code = "PF" },
            new() { Id = new Guid("3f56da04-0993-49f6-98c3-5ca001a37c0f"), Name = "French Southern Territories", Code = "TF" },
            new() { Id = new Guid("b49714b4-e99a-45e7-bb8e-8d5380e3e773"), Name = "Gabon", Code = "GA" },
            new() { Id = new Guid("f8ae0b90-e85e-4e00-a92a-a92a1c6dccce"), Name = "Gambia", Code = "GM" },
            new() { Id = new Guid("61f78edf-fb5b-468e-92d2-5109abc99d2c"), Name = "Georgia", Code = "GE" },
            new() { Id = new Guid("7788f730-d39c-4fe1-9044-fa924bddd714"), Name = "Germany", Code = "DE" },
            new() { Id = new Guid("16831f08-ef75-4e3d-840e-ae2fba854230"), Name = "Ghana", Code = "GH" },
            new() { Id = new Guid("aed13659-2e08-4f09-b9ca-29a9c1f4367b"), Name = "Gibraltar", Code = "GI" },
            new() { Id = new Guid("a8f460b6-368f-41c8-a72d-1e82d489d0a1"), Name = "Greece", Code = "GR" },
            new() { Id = new Guid("1160a8d3-2e5d-4849-bb33-f119e3a9a8c8"), Name = "Greenland", Code = "GL" },
            new() { Id = new Guid("8925e1aa-cfce-45e3-9812-1d7a5f6f8f24"), Name = "Grenada", Code = "GD" },
            new() { Id = new Guid("7577652b-ef79-48e1-aafa-35140192ddb2"), Name = "Guadeloupe", Code = "GP" },
            new() { Id = new Guid("4ecfdc89-12ca-4dd5-892f-f53d1bcc7ff0"), Name = "Guam", Code = "GU" },
            new() { Id = new Guid("28df0cdd-f7d9-4552-ba2f-f394e83dc3b7"), Name = "Guatemala", Code = "GT" },
            new() { Id = new Guid("a72dcdd4-91aa-4ef7-9300-c3a3c3b113db"), Name = "Guernsey", Code = "GG" },
            new() { Id = new Guid("944b712d-78dc-4412-aa25-de65f37c79ff"), Name = "Guinea", Code = "GN" },
            new() { Id = new Guid("bf5e382d-7b77-4255-8085-226fad40f05d"), Name = "Guinea-Bissau", Code = "GW" },
            new() { Id = new Guid("4bfd0ccd-ca5b-4d3b-9781-c4d74e944468"), Name = "Guyana", Code = "GY" },
            new() { Id = new Guid("474a01ff-a828-44d5-af38-166f79e550a3"), Name = "Haiti", Code = "HT" },
            new() { Id = new Guid("63628493-ba12-4ef3-a211-d17f1f444213"), Name = "Heard Island and McDonald Islands", Code = "HM" },
            new() { Id = new Guid("b77e2e75-9530-4f8a-bcca-05bc527e6763"), Name = "Holy See (Vatican City State)", Code = "VA" },
            new() { Id = new Guid("715f02ea-6f0b-41d2-9f03-779791322a37"), Name = "Honduras", Code = "HN" },
            new() { Id = new Guid("e98a9b22-5f3f-4f49-a3fb-d84fc6914d03"), Name = "Hong Kong", Code = "HK" },
            new() { Id = new Guid("3363e45b-8e36-4234-bda9-cf09dd304b28"), Name = "Hungary", Code = "HU" },
            new() { Id = new Guid("a047d100-d71b-49ff-adcf-e814005d34b5"), Name = "Iceland", Code = "IS" },
            new() { Id = new Guid("4e2fe43e-3dfb-49e8-bc85-6bce738e7d83"), Name = "India", Code = "IN" },
            new() { Id = new Guid("24bee380-0814-4cc7-a54a-24f1334612a7"), Name = "Indonesia", Code = "ID" },
            new() { Id = new Guid("3955f693-5123-41b2-b06a-ab27e483b8dd"), Name = "Iran, Islamic Republic of", Code = "IR" },
            new() { Id = new Guid("fc8a155e-dbbd-4279-a7c0-d35df2a3ea8d"), Name = "Iraq", Code = "IQ" },
            new() { Id = new Guid("8c0f415d-833c-4286-bc26-164a909b6129"), Name = "Ireland", Code = "IE" },
            new() { Id = new Guid("94cf5ba2-b976-430f-b2aa-4ed515edc3c4"), Name = "Isle of Man", Code = "IM" },
            new() { Id = new Guid("b48e8abb-894c-4c16-8111-025fb5d9a870"), Name = "Israel", Code = "IL" },
            new() { Id = new Guid("4986aa6d-1f3a-4fbc-a3df-143daf3f06d0"), Name = "Italy", Code = "IT" },
            new() { Id = new Guid("2715a417-1f6c-45d5-ae74-b977dc0f3e6e"), Name = "Jamaica", Code = "JM" },
            new() { Id = new Guid("b05e0c58-4bb0-4959-ab23-0353f701cf9d"), Name = "Japan", Code = "JP" },
            new() { Id = new Guid("72b17ecf-6eef-41fd-aa94-4faf50479817"), Name = "Jersey", Code = "JE" },
            new() { Id = new Guid("dcf1b4fa-d526-4d50-9af1-c0c285cfef7c"), Name = "Jordan", Code = "JO" },
            new() { Id = new Guid("8f497887-3669-4c18-a4c9-c16f7bf6f54e"), Name = "Kazakhstan", Code = "KZ" },
            new() { Id = new Guid("510cc27e-379a-456c-ad75-67ca9b75059c"), Name = "Kenya", Code = "KE" },
            new() { Id = new Guid("1953b73b-6f75-4bfd-866b-f750576add1a"), Name = "Kiribati", Code = "KI" },
            new() { Id = new Guid("7f0f1a37-0956-4d88-baa7-d3120f3f4353"), Name = "Korea, Democratic People's Republic of", Code = "KP" },
            new() { Id = new Guid("3862f909-2ac4-45c6-8ac3-1eb2c99d4eda"), Name = "Korea, Republic of", Code = "KR" },
            new() { Id = new Guid("78eeefdb-d438-4120-8270-007cbcd6cc21"), Name = "Kuwait", Code = "KW" },
            new() { Id = new Guid("86fe2212-9c77-4ea1-9bc3-c53d1539ac08"), Name = "Kyrgyzstan", Code = "KG" },
            new() { Id = new Guid("85024048-9a3e-472e-9352-0605f8d32c9c"), Name = "Lao People's Democratic Republic", Code = "LA" },
            new() { Id = new Guid("61511546-d634-4120-86fd-7e05fcf1737e"), Name = "Latvia", Code = "LV" },
            new() { Id = new Guid("7f65261b-9266-4399-ab4b-e6e54434559f"), Name = "Lebanon", Code = "LB" },
            new() { Id = new Guid("e84f38da-9605-47e8-b7bd-fec197d7a6b7"), Name = "Lesotho", Code = "LS" },
            new() { Id = new Guid("bc2a42f4-8cab-40d9-871f-01b90ab1f2d0"), Name = "Liberia", Code = "LR" },
            new() { Id = new Guid("82306842-b21a-44f6-af95-3a033690ccf7"), Name = "Libya", Code = "LY" },
            new() { Id = new Guid("dc075eab-e84f-49e5-8356-6f3057c0184a"), Name = "Liechtenstein", Code = "LI" },
            new() { Id = new Guid("c5781e3c-5047-4dca-b610-b6204d06c674"), Name = "Lithuania", Code = "LT" },
            new() { Id = new Guid("eb912e25-4fa5-4b7e-ae06-50ae4de5935b"), Name = "Luxembourg", Code = "LU" },
            new() { Id = new Guid("c51dbc45-8114-42e8-91b4-983e73882ac6"), Name = "Macao", Code = "MO" },
            new() { Id = new Guid("680656a3-1db8-4a04-843a-2236449adf8c"), Name = "Macedonia, the Former Yugoslav Republic of", Code = "MK" },
            new() { Id = new Guid("aa1de2c5-50c8-4970-8edd-4998b2874fea"), Name = "Madagascar", Code = "MG" },
            new() { Id = new Guid("4e4d6576-b09f-47ba-b859-8038fdb2e826"), Name = "Malawi", Code = "MW" },
            new() { Id = new Guid("2e0e9a6c-13d6-43a1-8c02-7dabbbcf72b0"), Name = "Malaysia", Code = "MY" },
            new() { Id = new Guid("3e824cd5-4bc7-4b8a-9af2-908e07fb500b"), Name = "Maldives", Code = "MV" },
            new() { Id = new Guid("37bdb9ce-053e-4c08-87ce-c1df750c1088"), Name = "Mali", Code = "ML" },
            new() { Id = new Guid("dea40973-b08a-43ca-92c5-b651964fa7d6"), Name = "Malta", Code = "MT" },
            new() { Id = new Guid("1062904d-f1e1-4e21-bf5c-2c62fa451160"), Name = "Marshall Islands", Code = "MH" },
            new() { Id = new Guid("05dcba10-4264-4f15-bf80-a1fe3d9464d7"), Name = "Martinique", Code = "MQ" },
            new() { Id = new Guid("e2a54084-9ca9-4f9b-8998-bfdd68895630"), Name = "Mauritania", Code = "MR" },
            new() { Id = new Guid("1dd51bf7-1fcf-421e-a1a1-f8a94e533fe7"), Name = "Mauritius", Code = "MU" },
            new() { Id = new Guid("8950474b-7617-479f-b713-7a3ea8cc9c56"), Name = "Mayotte", Code = "YT" },
            new() { Id = new Guid("16bea471-80e5-4923-839c-b9fd8b23196a"), Name = "Mexico", Code = "MX" },
            new() { Id = new Guid("2e69695d-2398-4ca5-8def-6ac00f52991d"), Name = "Micronesia, Federated States of", Code = "FM" },
            new() { Id = new Guid("9000b061-2f80-42d9-9a12-ec1a987f2c77"), Name = "Moldova, Republic of", Code = "MD" },
            new() { Id = new Guid("181a4981-76f7-4571-b3be-aa5f8188c4cd"), Name = "Monaco", Code = "MC" },
            new() { Id = new Guid("7666de6e-8cbd-4b56-bc7d-8475838b1a32"), Name = "Mongolia", Code = "MN" },
            new() { Id = new Guid("0f87c718-302a-4708-8022-4b4f96034445"), Name = "Montenegro", Code = "ME" },
            new() { Id = new Guid("31f6fc9d-30fc-4c4c-ae93-9e4258a6172a"), Name = "Montserrat", Code = "MS" },
            new() { Id = new Guid("92339a57-5d5f-4533-ad41-9dfcc75a82d9"), Name = "Morocco", Code = "MA" },
            new() { Id = new Guid("7b772ea3-a8fd-44b3-8146-3abc411eecb2"), Name = "Mozambique", Code = "MZ" },
            new() { Id = new Guid("e4a15f59-2263-4151-af4a-c9634e3ea0f2"), Name = "Myanmar", Code = "MM" },
            new() { Id = new Guid("3a618b17-46cf-445d-b19d-e0ae8e679347"), Name = "Namibia", Code = "NA" },
            new() { Id = new Guid("53328a78-a760-4491-9644-bea760b8da36"), Name = "Nauru", Code = "NR" },
            new() { Id = new Guid("02c2d511-5979-4d26-a0dc-9bffd0d4d210"), Name = "Nepal", Code = "NP" },
            new() { Id = new Guid("367b8a21-8924-4707-80ae-b0322bf0ccd8"), Name = "Netherlands", Code = "NL" },
            new() { Id = new Guid("7f0f6199-39c9-42b1-9171-519b0d28b35b"), Name = "New Caledonia", Code = "NC" },
            new() { Id = new Guid("ee5c1962-bded-43f0-baa1-e86a8ddf2875"), Name = "New Zealand", Code = "NZ" },
            new() { Id = new Guid("b8184e5d-5d72-43e7-983a-49b234c818ac"), Name = "Nicaragua", Code = "NI" },
            new() { Id = new Guid("66dbe434-d2dc-4f68-b7ac-9a82bd50b3f1"), Name = "Niger", Code = "NE" },
            new() { Id = new Guid("4604e99b-834f-4e78-b7d1-ca358e238680"), Name = "Nigeria", Code = "NG" },
            new() { Id = new Guid("f3b01d75-a5b0-4804-bab7-3f5cccc9e163"), Name = "Niue", Code = "NU" },
            new() { Id = new Guid("cb005bfb-7215-4840-8c2a-8084f7bc8b75"), Name = "Norfolk Island", Code = "NF" },
            new() { Id = new Guid("85719beb-dc63-4b1e-8271-61c9c053e016"), Name = "Northern Mariana Islands", Code = "MP" },
            new() { Id = new Guid("ab1bf40f-59c9-4466-87eb-a05bad6e3eb7"), Name = "Norway", Code = "NO" },
            new() { Id = new Guid("3e2baff7-1931-4926-908f-d96c8b1017ec"), Name = "Oman", Code = "OM" },
            new() { Id = new Guid("d13f92f3-9c8b-4740-92ee-1e7f0dce7856"), Name = "Pakistan", Code = "PK" },
            new() { Id = new Guid("edbe9e35-5823-4c11-81c4-33392a382bc5"), Name = "Palau", Code = "PW" },
            new() { Id = new Guid("62b97dfb-431f-4f24-ae69-0e538cb351ce"), Name = "Palestine, State of", Code = "PS" },
            new() { Id = new Guid("ffd702b8-4790-4928-8c85-0098917ada97"), Name = "Panama", Code = "PA" },
            new() { Id = new Guid("812cc990-e063-43fa-b589-3903f0419b2b"), Name = "Papua New Guinea", Code = "PG" },
            new() { Id = new Guid("201ca2ea-24a3-465f-8247-1cdac991ba99"), Name = "Paraguay", Code = "PY" },
            new() { Id = new Guid("39c2db0c-b84a-4aec-957a-b1e74ed970ee"), Name = "Peru", Code = "PE" },
            new() { Id = new Guid("d46526b9-efd4-4b0c-8a44-df8156becccd"), Name = "Philippines", Code = "PH" },
            new() { Id = new Guid("016dac7f-20b2-4ec0-9afc-59673e1b3f10"), Name = "Pitcairn", Code = "PN" },
            new() { Id = new Guid("c1897adc-3b07-4910-a7b2-09dd4d14d35d"), Name = "Poland", Code = "PL" },
            new() { Id = new Guid("4d5875ff-211a-45fb-9772-a8a2f97f127c"), Name = "Portugal", Code = "PT" },
            new() { Id = new Guid("4be3837f-c8c0-4469-922d-212d1ffbb284"), Name = "Puerto Rico", Code = "PR" },
            new() { Id = new Guid("7bf5ac7b-6da9-45de-b9e0-53976e04ad38"), Name = "Qatar", Code = "QA" },
            new() { Id = new Guid("3f219446-6df0-470c-b2b1-fb0b476f7c02"), Name = "Réunion", Code = "RE" },
            new() { Id = new Guid("d401972f-15de-4113-9cfc-3d130544c000"), Name = "Romania", Code = "RO" },
            new() { Id = new Guid("a97de304-71f2-48d6-964f-eec28f461656"), Name = "Russian Federation", Code = "RU" },
            new() { Id = new Guid("82747272-eda9-42e9-9a08-efaafaab992e"), Name = "Rwanda", Code = "RW" },
            new() { Id = new Guid("008ac456-2f5e-45cb-9622-0fbed7e16af2"), Name = "Saint Barthélemy", Code = "BL" },
            new() { Id = new Guid("afdd6b43-c515-48b6-8c50-673ee81969e0"), Name = "Saint Helena, Ascension and Tristan da Cunha", Code = "SH" },
            new() { Id = new Guid("f821e262-370c-45f9-a89c-b2171768e172"), Name = "Saint Kitts and Nevis", Code = "KN" },
            new() { Id = new Guid("ad5ac333-c5ae-4bf6-ad13-9129f1026542"), Name = "Saint Lucia", Code = "LC" },
            new() { Id = new Guid("0c80b77b-042f-46bc-8270-5166ed46f7c0"), Name = "Saint Martin (French part)", Code = "MF" },
            new() { Id = new Guid("e640a534-58d3-4ba6-a519-80173719909b"), Name = "Saint Pierre and Miquelon", Code = "PM" },
            new() { Id = new Guid("7826a073-3e81-4ffa-860d-92fc822b0bd6"), Name = "Saint Vincent and the Grenadines", Code = "VC" },
            new() { Id = new Guid("76a589de-5d51-4242-bbf4-cb3dfd627c13"), Name = "Samoa", Code = "WS" },
            new() { Id = new Guid("058a5789-32c5-4095-b6cd-184cdef4ef9a"), Name = "San Marino", Code = "SM" },
            new() { Id = new Guid("410cc59a-a2b8-429a-b9f5-88aed34d22be"), Name = "Sao Tome and Principe", Code = "ST" },
            new() { Id = new Guid("7d42e6e0-1019-4d27-b21a-5325a5510b24"), Name = "Saudi Arabia", Code = "SA" },
            new() { Id = new Guid("bc000f63-8ca3-4b29-8126-5943c209c5a0"), Name = "Senegal", Code = "SN" },
            new() { Id = new Guid("970d18e0-8d0c-4e4f-b697-71d8f1bb7bb4"), Name = "Serbia", Code = "RS" },
            new() { Id = new Guid("d6d2414d-64e5-4703-a8d8-d8aabc3c63e8"), Name = "Seychelles", Code = "SC" },
            new() { Id = new Guid("bb3bce54-17ca-4881-ae46-354d06312403"), Name = "Sierra Leone", Code = "SL" },
            new() { Id = new Guid("cbd1cb2d-6fff-4635-a7f0-0d6e443e4949"), Name = "Singapore", Code = "SG" },
            new() { Id = new Guid("4f4e241b-3c8a-4b38-a3f3-60dca1692733"), Name = "Sint Maarten (Dutch part)", Code = "SX" },
            new() { Id = new Guid("4118edf4-1212-40c3-a3c7-73f1394592e5"), Name = "Slovakia", Code = "SK" },
            new() { Id = new Guid("fb149453-6bb1-4980-851d-20c4bf26387d"), Name = "Slovenia", Code = "SI" },
            new() { Id = new Guid("08f8dc93-710d-442e-bbef-3670ebed0f34"), Name = "Solomon Islands", Code = "SB" },
            new() { Id = new Guid("661895c3-1921-4f1c-9650-ede4c49545bd"), Name = "Somalia", Code = "SO" },
            new() { Id = new Guid("5b2dbfe6-86d1-43d4-b9c4-a10de5f7909e"), Name = "South Africa", Code = "ZA" },
            new() { Id = new Guid("eb287298-f0fa-43a6-a33f-1024ea7498e7"), Name = "South Georgia and the South Sandwich Islands", Code = "GS" },
            new() { Id = new Guid("61000536-dbcc-45d7-a33f-854c4b8c259c"), Name = "South Sudan", Code = "SS" },
            new() { Id = new Guid("5cc1880f-eb9d-4350-9d25-7febbdd2e316"), Name = "Spain", Code = "ES" },
            new() { Id = new Guid("f69e8378-af14-4c6a-ab98-214dd56c9ffe"), Name = "Sri Lanka", Code = "LK" },
            new() { Id = new Guid("4743f921-3dc0-469a-995d-906e9cd14433"), Name = "Sudan", Code = "SD" },
            new() { Id = new Guid("9711b45a-4469-4043-a30e-ce7104206e56"), Name = "Suriname", Code = "SR" },
            new() { Id = new Guid("c2869921-a1cb-4986-b05c-bdb46f83b841"), Name = "Svalbard and Jan Mayen", Code = "SJ" },
            new() { Id = new Guid("f445d50e-1c7c-40c7-ad7c-b3f6ebedead4"), Name = "Swaziland", Code = "SZ" },
            new() { Id = new Guid("2b4f3fa5-6370-4514-b0e2-04308df16f35"), Name = "Sweden", Code = "SE" },
            new() { Id = new Guid("da055af9-a44d-49b4-9ad2-bb86b14b1620"), Name = "Switzerland", Code = "CH" },
            new() { Id = new Guid("dcd1c66e-fd1d-457b-a6bd-7b87fd20eee2"), Name = "Syrian Arab Republic", Code = "SY" },
            new() { Id = new Guid("67a0075f-677e-414d-b26f-2ddc91ae1452"), Name = "Taiwan, Province of China", Code = "TW" },
            new() { Id = new Guid("83f7c5f5-a7f1-47b0-b3d1-91954f2f3afb"), Name = "Tajikistan", Code = "TJ" },
            new() { Id = new Guid("0dc2c701-3aea-4b37-9f68-3f0f1290ac3e"), Name = "Tanzania, United Republic of", Code = "TZ" },
            new() { Id = new Guid("e0dde8e5-fe62-44ba-9915-ab8ca3394302"), Name = "Thailand", Code = "TH" },
            new() { Id = new Guid("3b0eab17-c3bc-4c73-8225-4574a4472ce3"), Name = "Timor-Leste", Code = "TL" },
            new() { Id = new Guid("47acbe65-7c8f-4eb9-a479-a93127da4778"), Name = "Togo", Code = "TG" },
            new() { Id = new Guid("5cfcae12-cf67-4a0d-8903-c3f9dac4fd2c"), Name = "Tokelau", Code = "TK" },
            new() { Id = new Guid("c5c2325d-c2dc-4fae-8ced-2d23ba4ad5a8"), Name = "Tonga", Code = "TO" },
            new() { Id = new Guid("6458b859-a583-49f1-a9fe-5eafa82d235e"), Name = "Trinidad and Tobago", Code = "TT" },
            new() { Id = new Guid("5287c03a-f147-44d8-92f3-6bb74acdf4bf"), Name = "Tunisia", Code = "TN" },
            new() { Id = new Guid("6b4df40d-273b-4368-b86b-1eaf1a830a52"), Name = "Turkey", Code = "TR" },
            new() { Id = new Guid("72c29e25-7d35-44cd-88b4-6f0e7cdd88cb"), Name = "Turkmenistan", Code = "TM" },
            new() { Id = new Guid("bc95ba2a-f7b6-4fa6-8c1e-a581d1f62a32"), Name = "Turks and Caicos Islands", Code = "TC" },
            new() { Id = new Guid("3a26e76b-615d-4f6b-aaca-2f29a2b90234"), Name = "Tuvalu", Code = "TV" },
            new() { Id = new Guid("077bd666-9933-4120-a9cc-aa30da5edcc7"), Name = "Uganda", Code = "UG" },
            new() { Id = new Guid("13356e2d-32a4-49a3-98d3-acc91c5bb527"), Name = "Ukraine", Code = "UA" },
            new() { Id = new Guid("0be25ff0-eb9b-4d2b-9556-8f7c560fe92d"), Name = "United Arab Emirates", Code = "AE" },
            new() { Id = new Guid("9d0eb010-6cb4-459d-be6d-6944fda64ddd"), Name = "United Kingdom", Code = "GB" },
            new() { Id = new Guid("5f0d8d48-e8f3-44ef-bf1c-aff5881a7309"), Name = "United States", Code = "US" },
            new() { Id = new Guid("c6ceea68-fab0-4fce-b409-c45f41271c21"), Name = "United States Minor Outlying Islands", Code = "UM" },
            new() { Id = new Guid("a21871f6-0214-4f2a-abcb-125bb9bbb2d4"), Name = "Uruguay", Code = "UY" },
            new() { Id = new Guid("01d10e90-8470-4a59-a990-d6b5a302a56b"), Name = "Uzbekistan", Code = "UZ" },
            new() { Id = new Guid("a071b514-ac35-43b4-a5d7-fdf8d679bbed"), Name = "Vanuatu", Code = "VU" },
            new() { Id = new Guid("6dff409f-a36f-4a51-bcf4-416235585cee"), Name = "Venezuela , Bolivarian Republic of", Code = "VE" },
            new() { Id = new Guid("0ddcf8b9-0763-470d-a781-0c46aec0bb68"), Name = "Viet Nam", Code = "VN" },
            new() { Id = new Guid("77770a60-4d4b-4a99-8864-071487f79776"), Name = "Virgin Islands, British", Code = "VG" },
            new() { Id = new Guid("dbe5b573-13e3-4056-b0ef-b8ec3bfad3f0"), Name = "Virgin Islands, U.S.", Code = "VI" },
            new() { Id = new Guid("6c8c0413-6caf-4183-a0c8-4c775a005641"), Name = "Wallis and Futuna", Code = "WF" },
            new() { Id = new Guid("53093863-c095-417f-a4bd-22c32087cb41"), Name = "Western Sahara", Code = "EH" },
            new() { Id = new Guid("70223467-8bbc-4c4e-bb89-b3e309ded045"), Name = "Yemen", Code = "YE" },
            new() { Id = new Guid("04a7088c-a3c0-4b01-8bd3-0b31df17f96d"), Name = "Zambia", Code = "ZM" },
            new() { Id = new Guid("56989dab-0089-46c9-b6b2-01cb4f92a88d"), Name = "Zimbabwe", Code = "ZW" }
        };
    }
}
