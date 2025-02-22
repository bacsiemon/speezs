using speezs.DataAccess.Models;
using speezs.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess
{
	public class UnitOfWork
	{
		private SpeezsDbContext _db;

		public UnitOfWork(SpeezsDbContext db)
		{
			_db = db;
		}

		private CollectionLookRepository _collectionLookRepository;
		private FavoriteCollectionRepository _favoriteCollectionRepository;
		private LookProductRepository _lookProductRepository;
		private LookRepository _lookRepository;
		private MakeupProductRepository _makeupProductRepository;
		private ReviewRepository _reviewRepository;
		private SubscriptionTierRepository _subscriptionTierRepository;
		private TransferRepository _transferRepository;
		private UserPreferenceRepository _userPreferenceRepository;
		private UserRepository _userRepository;
		private UserSubscriptionRepository _userSubscriptionRepository;
		private UserResetPasswordCodeRepository _userResetPasswordCodeRepository;

		public CollectionLookRepository CollectionLookRepository { get { return _collectionLookRepository ??= new(_db); } }
		public FavoriteCollectionRepository FavoriteCollectionRepository { get { return _favoriteCollectionRepository ??= new(_db); } }
		public LookProductRepository LookProductRepository { get { return _lookProductRepository ??= new(_db); } }	
		public LookRepository LookRepository { get { return _lookRepository ??= new(_db); } }
		public MakeupProductRepository MakeupProductRepository { get { return _makeupProductRepository ??= new(_db); } }
		public ReviewRepository ReviewRepository { get { return _reviewRepository ??= new(_db); } }
		public SubscriptionTierRepository SubscriptionTierRepository { get { return _subscriptionTierRepository ??= new(_db); } }
		public TransferRepository TransferRepository { get { return _transferRepository ??= new(_db); } }
		public UserPreferenceRepository UserPreferenceRepository { get { return _userPreferenceRepository ??= new(_db); } }
		public UserRepository UserRepository { get { return _userRepository ??= new(_db); } }
		public UserSubscriptionRepository UserSubscriptionRepository { get { return _userSubscriptionRepository ??= new(_db); } }
		public UserResetPasswordCodeRepository UserResetPasswordCodeRepository { get { return _userResetPasswordCodeRepository ??= new(_db); } }
	}
}
