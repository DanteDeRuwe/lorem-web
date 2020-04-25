﻿using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace G09projectenII.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public AdminPanelController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        public IActionResult Index() => View(_sessionRepository.GetAll());
    }
}