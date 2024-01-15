﻿using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessagesController : BaseApiController
{
    private readonly IUnitOfWork _iow;
    private readonly IMapper _mapper;

    public MessagesController(IUnitOfWork iow, IMapper mapper)
    {
        _iow = iow;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMEssage(CreateMessageDto createMessageDto){
        var username = User.GetUsername();

        if(username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("You cannot send messages to yourself");

        var sender = await _iow.UserRepository.GetUserByUsernameAsync(username);
        var recipient = await _iow.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        if(recipient == null) return NotFound();

        var message = new Message{
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        _iow.MessageRepository.AddMessage(message);

        if(await _iow.Complete()) return Ok(_mapper.Map<MessageDto>(message));

        return BadRequest("Failed to send message");
    }


    [HttpGet]
    public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams){

        messageParams.username = User.GetUsername();

        var messages = await _iow.MessageRepository.GetMessagesForUser(messageParams);

        Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));

        return messages;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id){
        var username = User.GetUsername();
        
        var message = await _iow.MessageRepository.GetMessage(id);

        if(message.SenderUsername != username && message.RecipientUsername != username) 
        return Unauthorized();


        if(message.SenderUsername == username) message.SenderDeleted = true;
        if(message.RecipientUsername == username) message.RecipientDeleted = true;

        if(message.SenderDeleted && message.RecipientDeleted)
        {
            _iow.MessageRepository.DeleteMessage(message);
        }

        if(await _iow.Complete()) return Ok();

        return BadRequest("Problem deleting the message");



    }
}
