using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.DbContexts;
using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Services;

namespace RestaurantReservationAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/menu-items")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemController(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAllMenuItems()
        {
            IEnumerable<MenuItem> menuItems = await _menuItemRepository.GetAllMenuItemsAsync();

            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItems));

        }

        [HttpGet("{id}", Name = "GetMenuItem")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItem(int id)
        {
            MenuItem? menuItemToReturn = await _menuItemRepository.GetMenuItemAsync(id);

            if (menuItemToReturn == null)
            {
                return NotFound($"Menu item with ID {id} was not found.");
            }

            return Ok(_mapper.Map<MenuItemDto>(menuItemToReturn));


        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> CreateMenuItem(MenuItemCreateDto menuItem)
        {
            var finalMenuItem = _mapper.Map<Entities.MenuItem>(menuItem);

            await _menuItemRepository.CreateMenuItemAsync(finalMenuItem);
            await _menuItemRepository.SaveChangesAsync();
            var createdMenuItemToReturn = _mapper.Map<Models.MenuItemDto>(finalMenuItem);

            return CreatedAtRoute("GetMenuItem",
                new
                {
                    id = createdMenuItemToReturn.MenuItemId

                },
                createdMenuItemToReturn
                );
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateMenuItem(int id, MenuItemForUpdateDto menuItemForUpdate)
        {
            var menuItemEntity = await _menuItemRepository.GetMenuItemAsync(id);

            if (menuItemEntity == null)
            {
                return NotFound($"Menu item with ID {id} was not found.");
            }

            _mapper.Map(menuItemForUpdate, menuItemEntity);


            await _menuItemRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateMenuItem(int id, JsonPatchDocument<MenuItemForUpdateDto> patchDocument)
        {
            var menuItem = await _menuItemRepository.GetMenuItemAsync(id);

            if (menuItem == null)
            {
                return NotFound($"Menu item with ID {id} was not found.");
            }

            var menuItemToPatch = _mapper.Map<MenuItemForUpdateDto>(menuItem);

            patchDocument.ApplyTo(menuItemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(menuItemToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(menuItemToPatch, menuItem);
            await _menuItemRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenuItem(int id)
        {
            var menuItemEntity = await _menuItemRepository.GetMenuItemAsync(id);

            if (menuItemEntity == null)
            {
                return NotFound($"Menu item with ID {id} was not found.");
            }

            _menuItemRepository.DeleteMenuItemAsync(menuItemEntity);
            await _menuItemRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

