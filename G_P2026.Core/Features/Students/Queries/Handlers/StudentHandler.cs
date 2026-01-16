using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_P2026.Core.Features.Students.Queries.Models;

namespace G_P2026.Core.Features.Students.Queries.Handlers
{
	public class StudentHandler : IRequestHandler<GetStudentListQuery, List<GetStudentListQuery>> // request, response 
	{
		public Task<List<GetStudentListQuery>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
		{
			// TODO: Implement handler logic
			return Task.FromResult(new List<GetStudentListQuery>());
		}
	}
}
