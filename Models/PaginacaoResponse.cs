namespace WFConfin.Models
{
    /* <T>  where T: class == Significa que está classe de PaginacaoResponde pode ser utilizada por qualquer entidade  */
    public class PaginacaoResponse<T> where T : class
    {
        /* criação de atributos:
            public IEnumerable<T> Dados { get; set; }
                public IEnumerable == interface de tipo de lista 
                <T> == tipo de lista genérica
                Dados { get; set; } == Vai conter a listagem do que retornares de dados durante uma consulta, portanto
                                       será para ele que passaremos a informação

           public long TotalLinhas{ get; set; }
                TotalLinhas{ get; set; } == total de linhas/itens que eu vou retornar conforme a pesquisa;

          public int skip { get; set; }
                A partir de onde eu estou começando a informar o dado;


         public int Take { get; set; }
                Quantidade de dados que serão retornados com base no skip
     

         */

        public IEnumerable<T> Dados { get; set; }

        public long TotalLinhas{ get; set; }

        public int skip { get; set; }

        public int Take { get; set; }

        public PaginacaoResponse(IEnumerable<T> dados, long totalLinhas, int skip, int take)
        {
            Dados = dados;
            TotalLinhas = totalLinhas;
            this.skip = skip;
            Take = take;
        }
    }
}
