using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
    }
    class FooterService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _uniteOfWork;

        public FooterService(IFooterRepository footerRepository,IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _uniteOfWork = unitOfWork;

        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }
    }
}
