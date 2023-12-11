using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            return _mapper.Map<List<Item>, List<ItemDto>>(lsItem);
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpPost("createitem")]
        public async Task<ActionResult<HttpStatusCode>> CreateItem([FromBody]ItemDto itemDto)
        {
            try
            {
                var item = _mapper.Map<ItemDto, Item>(itemDto);
                await _itemRepository.InsertAsync(item, true);
                return Ok(HttpStatusCode.Created);
            }
            catch
            {
                return Ok(HttpStatusCode.InternalServerError);
                throw;
            }
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpPut("updateitem")]
        public async Task<ActionResult<HttpStatusCode>> UpdateItem([FromBody] ItemDto itemDto)
        {
            try
            {
                var item = _mapper.Map<ItemDto, Item>(itemDto);
                await _itemRepository.UpdateAsync(item, true);
                return Ok(HttpStatusCode.OK);
            }
            catch
            {
                return Ok(HttpStatusCode.InternalServerError);
                throw;
            }
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpDelete("deleteitem/{id}")]
        public async Task<int> DeleteItem(int id)
        {
            try
            {
                var item = await _itemRepository.GetByIdAsync(id);
                if (item == null) return 0;
                await _itemRepository.DeleteAsync(item, true);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        [HttpGet("getdataloopup")]
        public async Task<ActionResult<Dictionary<string, object?>>> GetDataLookUp()
        {
            var dic = new Dictionary<string, object?>();
            dic["TenantCode"] = await _tenantRepository.GetTenantCodeLookUp();
            dic["TenantID"] = await _tenantRepository.GetDataLookUp();
            
            return dic;
        }
    }
}
