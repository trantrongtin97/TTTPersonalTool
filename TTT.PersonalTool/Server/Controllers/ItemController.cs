using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTT.PersonalTool.Server.Repositories;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICoreSystermTTT _systermTTT;
        private readonly IItemRepository _itemRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;

        public ItemController(ILogger<UserController> logger, 
            ICoreSystermTTT systermTTT, 
            IItemRepository itemRepository,
            ITenantRepository tenantRepository,
            IMapper mapper)
        {
            _logger = logger;
            _systermTTT = systermTTT;
            _itemRepository = itemRepository;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpGet("getallitem")]
        public async Task<ActionResult<List<ItemDto>>> GetAllItem()
        {
            var lsItem = await _itemRepository.GetListAsync();
            return _mapper.Map<List<ItemDto>>(lsItem);
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpDelete("deleteitem/{id}")]
        public async Task<int> DeleteItem(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if(item ==null) return 0;
            await _itemRepository.DeleteAsync(item,true);
            return 1;
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpGet("gettenantdataloopup")]
        public async Task<ActionResult<List<TenantLookUp>>> GetTenantDataLookUp()
        {
            return await _tenantRepository.GetDataLookUp();
        }
    }
}
