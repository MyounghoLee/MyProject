using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionHostBR : IHandleAuctionHost
    {
        public string DevID { get { return "halfclub"; } }
        public string AppID { get { return "borikids"; } }
        public string AppPassword { get { return "qhfl3014"; } }
        public string MemberID { get { return "borikids"; } }
        public string PartnerID { get { return "B_AUCTION2"; } }
        public int LimitQty { get { return 3; } }
        public string CSTel { get { return ""; } }

        public string Tic
        {
            get
            {
                //발급일자 : 2013-09-16일 오전 11시
                //종료일자 : 2013-12-14일 오전 11시
                //return "d0whMf3XNmvzUNinh4AI9YPmvWjLzP/Jw6u+k/1ntoYE8rIzhPfymUUVzkDxwoea28qD3hsbnoESm+vLC73Q+959XmNXD9TZNtxH/JwcSOiiAXSqJp+Jqj+du7n8rF/nORMsbgYt73UfO2oMiflesj6nwIOigVtCGu4PzeGFVUqiwOb6G+3NANOIE1ZCWrEjRG1bGY28a63Ru0jnlgsJHbg=";

                //발급일자 : 2013-12-13일 오전 11시
                //종료일자 : 무기한
                return "dzVuh/ETvH9p4w0llsTHZ/Uih6gAlZH5WTCj45b/MJbjId3H4voS+0UVPren1kaVPTnDCowRNbZ2xumkf/tEpMKuOt8rzC9de7KLJTdEpKH9tLKnkyL3dB+L80UMC464iu6zLnvrxpEEzox9gmCixkMBoStnWgtXn6C39No3f7/edBJdYJa8Gk8UTLJQkTsyfDj7B4tvCreNSID1JKZZlmA=";
            }
        }
    }
}
