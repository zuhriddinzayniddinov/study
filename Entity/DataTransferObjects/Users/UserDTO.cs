using EimzoApi.Models;
using Entity.Models;

namespace Entity.DataTransferObjects;

public record UserDTO(
    long id,
    List<SignMethod> signMethods,
    string firstname,
    string midlename,
    string lastname,
    long? structureid
);