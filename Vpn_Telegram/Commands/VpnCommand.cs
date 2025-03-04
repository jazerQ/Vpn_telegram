using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.VpnLibrary;

namespace Weather_bot.Commands
{
    public class VpnCommand
    {
        private readonly VpnAccessGlobal _vpnAccess;
        public VpnCommand(VpnAccessGlobal vpnAccess)
        {
            _vpnAccess = vpnAccess;
        }
        public async Task GetList() 
        {
            await _vpnAccess.GetListInbounds();
        }
        public async Task GetInboundById(int id) 
        {
            await _vpnAccess.GetInboundById(id);
        }
        public async Task GetInboundByEmail(string email) 
        {
            await _vpnAccess.GetInboundByEmail(email);
        }
        public async Task GetInboundByUserId(string userId) 
        {
            await _vpnAccess.GetInboundByUserId(userId);
        }
        public async Task AddInbound(string name) 
        {
            await _vpnAccess.AddUserToInbound(name);
        }
    }
}
