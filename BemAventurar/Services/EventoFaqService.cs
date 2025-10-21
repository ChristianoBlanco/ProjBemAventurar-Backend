using AutoMapper;
using BemAventurar.DTO;
using BemAventurar.Models;
using Dapper;
using System.Data.SqlClient;


namespace BemAventurar.Services
{
    public class EventoFaqService : IEventoFaqInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EventoFaqService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<EventoFaqDTO>>> ListarEventosFaq()
        {
            var response = new ResponseModel<List<EventoFaqDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var faqs = await connection.QueryAsync<EventoFaq>("SELECT * FROM Evento_faqs");
            response.Dados = _mapper.Map<List<EventoFaqDTO>>(faqs);
            response.Mensagem = "Lista de FAQs carregada com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<EventoFaqDTO>> BuscarEventoFaq(int eventoId, string pergunta_faq)
        {
            var response = new ResponseModel<EventoFaqDTO>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var faq = await connection.QueryFirstOrDefaultAsync<EventoFaq>(
                "SELECT * FROM Evento_faqs WHERE EventoID = @EventoID AND Pergunta_faq = @Pergunta_faq",
                new { EventoID = eventoId, Pergunta_faq = pergunta_faq });

            if (faq == null)
            {
                response.Mensagem = "FAQ não encontrado.";
                response.Status = true;
                return response;
            }

            response.Dados = _mapper.Map<EventoFaqDTO>(faq);
            response.Mensagem = "FAQ encontrado com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoFaqDTO>>> CriarEventoFaq(EventoFaqDTO eventoFaq)
        {
            var response = new ResponseModel<List<EventoFaqDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new
            {
                eventoFaq.EventoID,
                eventoFaq.Pergunta_faq,
                eventoFaq.Resposta_faq,
                CriadoEm = DateTime.UtcNow
            };

            await connection.ExecuteAsync(
                "INSERT INTO Evento_faqs (EventoID, Pergunta_faq, Resposta_faq, CriadoEm) " +
                "VALUES (@EventoID, @Pergunta_faq, @Resposta_faq, @CriadoEm)", parametros);

            var faqs = await connection.QueryAsync<EventoFaq>("SELECT * FROM Evento_Faqs");
            response.Dados = _mapper.Map<List<EventoFaqDTO>>(faqs);
            response.Mensagem = "FAQ criado com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoFaqDTO>>> AtualizarEventoFaq(EventoFaqDTO eventoFaq)
        {
            var response = new ResponseModel<List<EventoFaqDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var faqExistente = await connection.QueryFirstOrDefaultAsync<EventoFaq>(
                "SELECT * FROM Evento_faqs WHERE FaqId = @FaqId", new { eventoFaq.FaqId });

            if (faqExistente == null)
            {
                response.Mensagem = "FAQ não encontrado.";
                response.Status = true;
                return response;
            }

            var parametros = new
            {
                eventoFaq.EventoID,
                eventoFaq.Pergunta_faq,
                eventoFaq.Resposta_faq,
                eventoFaq.FaqId
            };

            await connection.ExecuteAsync(
                "UPDATE Evento_faqs SET EventoID = @EventoID, Pergunta_faq = @Pergunta_faq, " +
                "Resposta_faq = @Resposta_faq WHERE FaqId = @FaqId", parametros);

            var faqs = await connection.QueryAsync<EventoFaq>("SELECT * FROM Evento_faqs");
            response.Dados = _mapper.Map<List<EventoFaqDTO>>(faqs);
            response.Mensagem = "FAQ atualizado com sucesso.";
            response.Status = true;

            return response;
        }

        public async Task<ResponseModel<List<EventoFaqDTO>>> DeletarEventoFaq(int faqId)
        {
            var response = new ResponseModel<List<EventoFaqDTO>>();

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var faqExistente = await connection.QueryFirstOrDefaultAsync<EventoFaq>(
                "SELECT * FROM Evento_faqs WHERE FaqId = @FaqId", new { FaqId = faqId });

            if (faqExistente == null)
            {
                response.Mensagem = "FAQ não encontrado.";
                response.Status = true;
                return response;
            }

            await connection.ExecuteAsync(
                "DELETE FROM Evento_faqs WHERE FaqId = @FaqId", new { FaqId = faqId });

            var faqs = await connection.QueryAsync<EventoFaq>("SELECT * FROM Evento_faqs");
            response.Dados = _mapper.Map<List<EventoFaqDTO>>(faqs);
            response.Mensagem = "FAQ deletado com sucesso.";
            response.Status = true;

            return response;
        }
    }
}
