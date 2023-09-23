using Clinic.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Infrastructure.Appointment;

public interface IAppointmentService
{
    /// <summary>
    /// تنظیم قرار در تایم مشخص در صورت امکان
    /// فیلد IsOk صحت انجام کار را مشخص می کند
    /// </summary>
    /// <param name="request">اطلاعات ورودی درخواست برای ثبت قرار</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///در صورت انجام شدن قرار مقدار IsOk صحیح بوده و اطلاعات مربوط به تاریخ و شناسه قرار پر خواهند شد
    /// </returns>
    Task<SetAppointmentResponseViewModel> SetAppointment(SetAppointmentRequestViewModel request,
        CancellationToken cancellationToken);

    ///  <summary>
    ///  تنظیم قرار در اولین وقت موجود
    ///  فیلد IsOk صحت انجام کار را مشخص می کند
    ///  </summary>
    ///  <param name="request"></param>
    ///  <param name="cancellationToken"></param>
    ///  <returns>
    /// در صورت انجام شدن قرار مقدار IsOk صحیح بوده و اطلاعات مربوط به تاریخ و شناسه قرار پر خواهند شد
    ///  </returns>
    Task<SetAppointmentResponseViewModel> SetEarliestAppointment(SetEarliestAppointmentRequestViewModel request,
        CancellationToken cancellationToken);
}