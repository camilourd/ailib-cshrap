namespace ailib.search.multilevel
{
    public abstract class CodeDecodeMap<G, P>
    {
		public P[] decode(G[] genomes)
		{
			P[] v = new P[genomes.Length];
			for (int i = 0; i < genomes.Length; i++)
				v[i] = decode(genomes[i]);
			return v;
		}

		public G[] encode(P[] population)
		{
			G[] genomes = new G[population.Length];
			for (int i = 0; i < genomes.Length; i++)
				genomes[i] = encode(population[i]);
			return genomes;
		}

		public abstract P decode(G genome);
		public abstract G encode(P thing);
	}
}
