using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IRepo<AddressBookEntry> _repo;

    public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    [BindProperty]
    public UpdateAddressRequest UpdateAddressRequest { get; set; }

    public void OnGet(Guid id)
    {
        UpdateAddressRequest req = new UpdateAddressRequest();
        UpdateAddressRequest = req;

        EntryByIdSpecification spec = new EntryByIdSpecification(id);
        IReadOnlyList<AddressBookEntry> data = _repo.Find(spec); // get relevant address entry

        AddressBookEntry entry = data.FirstOrDefault();

        if (entry != null)
        {
            // change each field according to UpdateAddressRequest
            UpdateAddressRequest.Id = entry.Id;
            UpdateAddressRequest.Line1 = entry.Line1;
            UpdateAddressRequest.Line2 = entry.Line2;
            UpdateAddressRequest.City = entry.City;
            UpdateAddressRequest.State = entry.State;
            UpdateAddressRequest.PostalCode = entry.PostalCode;
        }
        else
        {
            System.Console.WriteLine("Error finding AddressBookEntry with ID " + id);
        }
    }

    public ActionResult OnPost()
    {
        _mediator.Send(UpdateAddressRequest);

        return Page();
    }
}