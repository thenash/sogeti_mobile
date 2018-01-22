using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Connect.Helpers {

    public class PermissionsInstance {

        #region Instance

        private static Lazy<IPermissions> _permissions;

        public static IPermissions Instance {
            get {
                if(_permissions == null) {
                    _permissions = new Lazy<IPermissions>(() => CrossPermissions.Current, LazyThreadSafetyMode.PublicationOnly);
                }

                return _permissions.Value;
            }
            // ReSharper disable once UnusedMember.Global
            set {
                _permissions = new Lazy<IPermissions>(() => value, LazyThreadSafetyMode.PublicationOnly);
            }
        }

        #endregion

        public static async Task<bool> HasOrGetsPermissionAsync(Permission permission) {
            try {
                PermissionStatus status = await Instance.CheckPermissionStatusAsync(permission);

                if(status != PermissionStatus.Granted) { //It has not been granted so lets ask

                    Dictionary<Permission, PermissionStatus> results = await Instance.RequestPermissionsAsync(permission);
                    status = results[permission];
                }

                return status == PermissionStatus.Granted;

            } catch(Exception ex) {
                Debug.WriteLine($"\nIn PermissionsInstance.HasPermissionAsync() - Exception checking or requesting permission {permission}:\n{ex}\n");
                return false;
            }
        }
    }
}