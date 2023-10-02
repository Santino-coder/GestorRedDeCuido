function currencyFormatter({ currency, value }) {
    const formatter = new Intl.NumberFormat('es-CR', {
        style: 'currency',
        minimumFractionDigits: 2,
        currency
    })
    return formatter.format(value)
}

const value = 0

const colon = currencyFormatter({
    currency: "CRC",
    value
}) 