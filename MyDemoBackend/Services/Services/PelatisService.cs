﻿using System.Collections.Generic;
using AutoMapper;
using Data.Interfaces;
using Data.Repositories;
using Messages;
using Models.Entities;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class PelatisService : IPelatisService
    {
        IPelatisRepository _pelatisRepository;
        IMapper _pelatisMapper;


        public PelatisService(
            IPelatisRepository pelatisRepository,
            IMapper pelatisMapper)
        {
            _pelatisRepository = pelatisRepository;
            _pelatisMapper = pelatisMapper;
        }

        public async Task<ListResponse<string>> GetOnomata()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> onomataPelaton = await _pelatisRepository.GetOnomata();
                response.SetSuccess(onomataPelaton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomata with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomata with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        public async Task<ListResponse<string>> GetTilefona()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefona = await _pelatisRepository.GetTilefona();
                response.SetSuccess(tilefona);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefona with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefona with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ListResponse<PelatisDto>> GetPelates()
        {
            ListResponse<PelatisDto> response = new ListResponse<PelatisDto>();
            try
            {
                List<Pelatis> pelates = await _pelatisRepository.GetPelates();
                var dtoAfterMapping = _pelatisMapper.Map<List<PelatisDto>>(pelates);

                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetPelates with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetPelates with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ListResponse<string>> GetTilefonaMeKodikous1()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefonaMeKodikous = await _pelatisRepository.GetTilefonaMeKodikous();
                response.SetSuccess(tilefonaMeKodikous);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous1 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous1 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ListResponse<string>> GetTilefonaMeKodikous2()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefona = await _pelatisRepository.GetTilefona();
                List<string> tilefonaMeKodikous2 =tilefona.Select(x => "2310" + x).ToList();
                response.SetSuccess(tilefonaMeKodikous2);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        } 
        public async Task<ListResponse<string>> GetOnomataApoK()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> onomataPelaton = await _pelatisRepository.GetOnomata();
                List<string> OnomataApoK = onomataPelaton
                    .Where(x =>x !=null && x.StartsWith("K"))
                    .ToList();
                    response.SetSuccess(OnomataApoK);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        public async Task<ListResponse<Tuple<string, List<int>, List<int>>>> GetOnomataIdPelatonKaiTimiKaseton()
        {
            ListResponse<Tuple<string, List<int>, List<int>>> response = new ListResponse<Tuple<string, List<int>, List<int>>>();
            try
            {
                List<Tuple<string, List<int>, List<int>>> OnomataIdPelatonKaiTimiKaseton = await _pelatisRepository.GetOnomataIdPelatonKaiTimiKaseton();
                response.SetSuccess(OnomataIdPelatonKaiTimiKaseton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
    }
}