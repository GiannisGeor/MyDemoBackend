using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repositories;
using Messages;
using Serilog;
using Services.Interfaces;

namespace Services.Services
{
    public class Tn_snService : ITn_snService
    {
        ITn_snRepository _tn_snRepository;
        public Tn_snService(
            ITn_snRepository tn_snRepository)
        {
            _tn_snRepository = tn_snRepository;
        }

        public async Task<ListResponse<Tuple<string, string>>> GetOnomataRolousSinteleston()
        {
            ListResponse<Tuple<string, string>>  response = new ListResponse<Tuple<string, string>>();
            try
            {
                List<Tuple<string, string>> OnomataRoloiSinteleston = await _tn_snRepository.GetOnomataRolousSinteleston();
                response.SetSuccess(OnomataRoloiSinteleston);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomataRolousSinteleston with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomataRolousSinteleston with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        public async Task<ObjectResponse<List<int>>> GetTainiaIdAlfred()

        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> TainiaIdAlfred = await _tn_snRepository.GetTainiaIdAlfred();
                response.SetSuccess(TainiaIdAlfred);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTainiaIdAlfred with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTainiaIdAlfred with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
    }
}
