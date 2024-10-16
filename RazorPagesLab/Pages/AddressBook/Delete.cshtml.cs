using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;

        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }
        public ActionResult OnGet(Guid id)
        {
            EntryByIdSpecification spec = new EntryByIdSpecification(id); // find appropriate entry
            IReadOnlyList<AddressBookEntry> data = _repo.Find(spec);

            AddressBookEntry entry = data.FirstOrDefault();

            _repo.Remove(entry); // delete entry from db

            return Page();
        }
    }
}
