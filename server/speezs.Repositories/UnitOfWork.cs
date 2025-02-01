using speezs.DataAccess.Models;
using speezs.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Repositories
{
	public class UnitOfWork
	{
		private SpeezsDbContext _context;
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

		public UnitOfWork()
		{
			_context ??= new SpeezsDbContext();
		}

		public CollectionLookRepository CollectionLookRepository { get { return _collectionLookRepository ??= new(); } }
		public FavoriteCollectionRepository FavoriteCollectionRepository { get { return _favoriteCollectionRepository ??= new(); } }
		public LookProductRepository LookProductRepository { get { return _lookProductRepository ??= new(); } }	
		public LookRepository LookRepository { get { return _lookRepository ??= new(); } }
		public MakeupProductRepository MakeupProductRepository { get { return _makeupProductRepository ??= new(); } }
		public ReviewRepository ReviewRepository { get { return _reviewRepository ??= new(); } }
		public SubscriptionTierRepository SubscriptionTierRepository { get { return _subscriptionTierRepository ??= new(); } }
		public TransferRepository TransferRepository { get { return _transferRepository ??= new(); } }
		public UserPreferenceRepository UserPreferenceRepository { get { return _userPreferenceRepository ??= new(); } }
		public UserRepository UserRepository { get { return _userRepository ??= new(); } }
		public UserSubscriptionRepository UserSubscriptionRepository { get { return _userSubscriptionRepository ??= new(); } }
	}
}
