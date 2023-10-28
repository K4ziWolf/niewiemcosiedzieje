using AutoMapper;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetAllClasses
{
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<ClassDto>>
    {
        private readonly IMapper _mapper;
        private readonly IHackHeroesRepository _repository;

        public GetAllClassesQueryHandler(IMapper mapper, IHackHeroesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var classes = await _repository.GetAllClasses();

            var dtos = _mapper.Map<IEnumerable<ClassDto>>(classes);

            var ownersDtos = dtos.Where(dto => dto.IsEditable == true);

            return ownersDtos;
        }
    }
}
