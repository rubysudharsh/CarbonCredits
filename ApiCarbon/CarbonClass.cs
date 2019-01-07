using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using TechTalk.SpecFlow;

namespace ApiCarbon
{
	[Binding]
	public class CarbonClass
	{
		private readonly RestClient client;
		private readonly RestRequest request;
		private readonly IRestResponse response;
		private Items Items;
		
		[BeforeScenario]
		public void setup()
		{
			RestClient client = new RestClient("https://api.tmsandbox.co.nz/v1/Categories/6327/Details.json?catalogue=false");
			JObject jsonResult = new JObject();

			RestRequest request = new RestRequest(Method.GET);
			IRestResponse response = client.Execute(request);
			response = client.Execute(request);

			JsonDeserializer deserializer = new JsonDeserializer();
			Items = deserializer.Deserialize<Items>(response);
		}

		[Given(@"The name is Carbon Credits")]
		public void GivenTheNameIsCarbonCredits()
		{
			String name = Items.Name;
			if(name.Equals("Carbon credits"))
			{
				Console.WriteLine("--------------The Name is Carbon Credits as expected--------------");
			}
			else
			{
				Console.WriteLine("--------------Name retrieves wrong value--------------");
			}
		}

		[Then(@"can be relisted")]
		public void ThenCanBeRelisted()
		{
			bool canRelist = Items.CanRelist;
			if (canRelist.Equals(true))
			{
				Console.WriteLine("-----------Can be relisted as expected----------");
			}
			else
			{
				Console.WriteLine("----------Cannot be relisted---------------");
			}
		}

		[Then(@"promotions element with Name : Gallery has a description which contains a text ""(.*)""")]
		public void ThenPromotionsElementWithNameGalleryHasADescriptionWhichContainsAText(string expectedTextInDescription)
		{
			IList<string> promotionsList = Items.Promotions;
			string[] obj = promotionsList.ToArray();

			for (int i = 0; i < obj.Length; i++)
			{
				if (obj[i].ToString().Contains("\"Name\":\"Gallery\"") & obj[i].ToString().Contains(expectedTextInDescription) == true)
				{
					Console.WriteLine($"--------------Expected text in the description is retrieved from {obj[i].ToString()}--------------");
					return;
				}
			}
		}
	}
}
