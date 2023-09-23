using Appointment.Domain;

namespace Appointment.Contract.UseCases;

public interface IAppointmentManager
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
    Task<SetAppointmentResponseDto> SetAppointment(SetAppointmentRequestDto request, CancellationToken cancellationToken);

    ///  <summary>
    ///  تنظیم قرار در اولین وقت موجود
    ///  فیلد IsOk صحت انجام کار را مشخص می کند
    ///  </summary>
    ///  <param name="request"></param>
    ///  <param name="cancellationToken"></param>
    ///  <returns>
    /// در صورت انجام شدن قرار مقدار IsOk صحیح بوده و اطلاعات مربوط به تاریخ و شناسه قرار پر خواهند شد
    ///  </returns>
    SetAppointmentResponseDto SetEarliestAppointment(SetEarliestAppointmentRequestDto request, CancellationToken cancellationToken);
}