
    using System;
using UniRx;
using UnityEngine;

namespace cova.ui.common
{
    /// <summary>
    /// View �̊��N���X
    /// </summary>
    public abstract class BaseView<TViewModel> : MonoBehaviour, IDisposable where TViewModel : BaseViewModel
    {
        protected readonly CompositeDisposable m_disposable = new CompositeDisposable();

        /// <summary>
        /// ���������\�b�h
        /// </summary>
        /// <param name="viewModel"></param>
        /// <typeparam name="T"></typeparam>
        public abstract void Initialize(TViewModel viewModel);

        public virtual void Dispose()
        {
            if (m_disposable.IsDisposed) return;
            m_disposable.Dispose();
        }
    }
}



