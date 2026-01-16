using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Core.Features.Students.Queries.Models
{
	public class GetStudentListQuery : IRequest<List<GetStudentListQuery>>
	{
	}
}
