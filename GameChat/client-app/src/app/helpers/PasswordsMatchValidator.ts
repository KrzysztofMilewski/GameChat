import { FormGroup, ValidatorFn, ValidationErrors } from '@angular/forms';

export const passwordMatchValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
    const password = control.get('password')
    const confirmPassword = control.get('confirmPassword')

    if (password.value === confirmPassword.value)
        return null
    else
        return { 'mismatch': true }
}
