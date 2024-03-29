﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Buscador.Test.UnitTests.Services.Stubs
{
	public class ConfigurationStub : IConfiguration
	{
		public string? this[string key] { get => "Site"; set => throw new NotImplementedException(); }

		public IEnumerable<IConfigurationSection> GetChildren()
		{
			throw new NotImplementedException();
		}

		public IChangeToken GetReloadToken()
		{
			throw new NotImplementedException();
		}

		public IConfigurationSection GetSection(string key)
		{
			throw new NotImplementedException();
		}

	}
}
