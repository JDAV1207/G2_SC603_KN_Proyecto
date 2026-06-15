/**
 * Orion Fit Studio — Management System
 * JavaScript Application (SPA)
 * Architecture: SOLID principles — Single Responsibility, Open/Closed,
 * Liskov Substitution, Interface Segregation, Dependency Inversion
 */

'use strict';

// ══════════════════════════════════════════════════════════════════════
// DATA STORE — Single source of truth (Single Responsibility Principle)
// ══════════════════════════════════════════════════════════════════════
const Store = (function () {
    const state = {
        currentUser: null,
        currentSection: 'dashboard',

        clients: [
            { id: 1, name: 'Keren Mora Quesada', cedula: '1-1723-0567', phone: '8830-8055', email: 'kerenmora14@gmail.com', address: 'El Carmen, La Unión, Cartago', date: '2024-01-10', status: 'Activa', expiry: '2025-05-10', avatar: 'KM', plan: 'Mensual' },
            { id: 2, name: 'Pamela Sibaja Torres', cedula: '1-1837-0889', phone: '8765-4321', email: 'pamela.sibaja@gmail.com', address: 'San José, Costa Rica', date: '2024-02-15', status: 'Activa', expiry: '2025-06-15', avatar: 'PS', plan: 'Trimestral' },
            { id: 3, name: 'Eduardo Poveda Bena.', cedula: '6-0408-0954', phone: '7654-3210', email: 'eduardo.poveda@gmail.com', address: 'Cartago, Costa Rica', date: '2024-03-01', status: 'Vencida', expiry: '2025-04-01', avatar: 'EP', plan: 'Mensual' },
            { id: 4, name: 'Oliver Román Quirós', cedula: '1-1900-0227', phone: '6543-2109', email: 'oliver.roman@gmail.com', address: 'Heredia, Costa Rica', date: '2023-11-20', status: 'Activa', expiry: '2025-08-20', avatar: 'OR', plan: 'Anual' },
            { id: 5, name: 'Juan Daniel Ávila', cedula: '1-1808-0063', phone: '5432-1098', email: 'juandaniel@gmail.com', address: 'San José, Costa Rica', date: '2024-04-05', status: 'Activa', expiry: '2025-05-15', avatar: 'JA', plan: 'Mensual' },
            { id: 6, name: 'Andrea Solís Bermúdez', cedula: '2-4321-8765', phone: '8123-4567', email: 'andrea.solis@gmail.com', address: 'Alajuela, Costa Rica', date: '2024-05-12', status: 'Activa', expiry: '2025-05-20', avatar: 'AS', plan: 'Mensual' },
            { id: 7, name: 'Carlos Murillo Fallas', cedula: '3-5678-1234', phone: '8234-5678', email: 'carlos.murillo@email.com', address: 'Liberia, Guanacaste', date: '2024-01-25', status: 'Suspendida', expiry: '2025-01-25', avatar: 'CM', plan: 'Mensual' },
            { id: 8, name: 'Diana Vega Porras', cedula: '4-8765-4321', phone: '8345-6789', email: 'diana.vega@email.com', address: 'Puntarenas, Costa Rica', date: '2024-06-01', status: 'Activa', expiry: '2025-06-01', avatar: 'DV', plan: 'Semestral' },
            { id: 9, name: 'Roberto Jiménez Mora', cedula: '5-2345-6789', phone: '8456-7890', email: 'roberto.j@email.com', address: 'San Carlos, Alajuela', date: '2024-07-10', status: 'Vencida', expiry: '2025-03-10', avatar: 'RJ', plan: 'Mensual' },
            { id: 10, 'name': 'Sofía Castro Arce', cedula: '1-6543-2187', phone: '8567-8901', email: 'sofia.castro@email.com', address: 'Santa Ana, San José', date: '2024-08-15', status: 'Activa', expiry: '2025-05-18', avatar: 'SC', plan: 'Trimestral' },
        ],

        memberships: [
            { id: 1, clientId: 1, plan: 'Mensual', start: '2025-04-10', end: '2025-05-10', amount: '₡25,000', status: 'Activa' },
            { id: 2, clientId: 2, plan: 'Trimestral', start: '2025-03-15', end: '2025-06-15', amount: '₡65,000', status: 'Activa' },
            { id: 3, clientId: 3, plan: 'Mensual', start: '2025-03-01', end: '2025-04-01', amount: '₡25,000', status: 'Vencida' },
            { id: 4, clientId: 4, plan: 'Anual', start: '2024-08-20', end: '2025-08-20', amount: '₡220,000', status: 'Activa' },
            { id: 5, clientId: 5, plan: 'Mensual', start: '2025-04-15', end: '2025-05-15', amount: '₡25,000', status: 'Activa' },
            { id: 6, clientId: 6, plan: 'Mensual', start: '2025-04-20', end: '2025-05-20', amount: '₡25,000', status: 'Activa' },
            { id: 7, clientId: 7, plan: 'Mensual', start: '2024-12-25', end: '2025-01-25', amount: '₡25,000', status: 'Suspendida' },
            { id: 8, clientId: 8, plan: 'Semestral', start: '2024-12-01', end: '2025-06-01', amount: '₡120,000', status: 'Activa' },
            { id: 9, clientId: 9, plan: 'Mensual', start: '2025-02-10', end: '2025-03-10', amount: '₡25,000', status: 'Vencida' },
            { id: 10, clientId: 10, plan: 'Trimestral', start: '2025-02-15', end: '2025-05-18', amount: '₡65,000', status: 'Activa' },
        ],

        payments: [
            { id: 1, clientId: 1, date: '2025-04-10', amount: '₡25,000', method: 'SINPE', plan: 'Mensual' },
            { id: 2, clientId: 2, date: '2025-03-15', amount: '₡65,000', method: 'Efectivo', plan: 'Trimestral' },
            { id: 3, clientId: 4, date: '2024-08-20', amount: '₡220,000', method: 'Tarjeta', plan: 'Anual' },
            { id: 4, clientId: 5, date: '2025-04-15', amount: '₡25,000', method: 'SINPE', plan: 'Mensual' },
            { id: 5, clientId: 6, date: '2025-04-18', amount: '₡25,000', method: 'SINPE', plan: 'Mensual' },
            { id: 6, clientId: 8, date: '2024-12-01', amount: '₡120,000', method: 'Tarjeta', plan: 'Semestral' },
            { id: 7, clientId: 10, date: '2025-02-15', amount: '₡65,000', method: 'Efectivo', plan: 'Trimestral' },
        ],

        wods: [
            {
                id: 1, name: 'AMRAP 20 — Fuego y Acero', date: '2025-04-18', category: 'AMRAP',
                desc: 'As Many Rounds As Possible en 20 minutos. Escala según tu nivel.',
                exercises: [
                    { name: 'Thrusters', sets: '3', reps: '10', weight: '40kg' },
                    { name: 'Pull-Ups', sets: '3', reps: '12', weight: 'BW' },
                    { name: 'Box Jumps', sets: '3', reps: '15', weight: '60cm' },
                ]
            },
            {
                id: 2, name: 'For Time — Hero WOD "Murph"', date: '2025-04-17', category: 'For Time',
                desc: 'Clásico WOD Murph. Con chaleco 9kg recomendado para Rx.',
                exercises: [
                    { name: '1 Mile Run', sets: '1', reps: '1600m', weight: '' },
                    { name: 'Pull-Ups', sets: '1', reps: '100', weight: 'BW' },
                    { name: 'Push-Ups', sets: '1', reps: '200', weight: 'BW' },
                    { name: 'Air Squats', sets: '1', reps: '300', weight: 'BW' },
                    { name: '1 Mile Run', sets: '1', reps: '1600m', weight: '' },
                ]
            },
            {
                id: 3, name: 'EMOM 15 — Fuerza Olímpica', date: '2025-04-16', category: 'EMOM',
                desc: 'Every Minute On the Minute. Descansa el tiempo restante del minuto.',
                exercises: [
                    { name: 'Clean & Jerk', sets: '1', reps: '3', weight: '60% 1RM' },
                    { name: 'Snatch', sets: '1', reps: '3', weight: '55% 1RM' },
                    { name: 'Dead Hang', sets: '1', reps: '30seg', weight: '' },
                ]
            },
            {
                id: 4, name: 'Chipper — La Bestia', date: '2025-04-15', category: 'Chipper',
                desc: 'Un round completo. Sin tiempo límite, pero compite contigo mismo.',
                exercises: [
                    { name: 'Deadlifts', sets: '1', reps: '50', weight: '80kg' },
                    { name: 'Burpees', sets: '1', reps: '40', weight: 'BW' },
                    { name: 'KBS Kettlebell', sets: '1', reps: '30', weight: '24kg' },
                    { name: 'Double Unders', sets: '1', reps: '200', weight: '' },
                ]
            },
        ],

        classes: [
            { id: 1, name: 'CrossFit Matutino', day: 'Lunes', time: '06:00', trainer: 'Esteban Guevara', max: 15, booked: 12, myReservation: false },
            { id: 2, name: 'Powerlifting', day: 'Lunes', time: '18:00', trainer: 'Esteban Guevara', max: 10, booked: 10, myReservation: false },
            { id: 3, name: 'CrossFit Intensivo', day: 'Martes', time: '07:00', trainer: 'Esteban Guevara', max: 15, booked: 8, myReservation: true },
            { id: 4, name: 'Olympic Lifting', day: 'Miércoles', time: '06:00', trainer: 'Esteban Guevara', max: 12, booked: 7, myReservation: false },
            { id: 5, name: 'Gymnastics & Skill', day: 'Jueves', time: '18:30', trainer: 'Esteban Guevara', max: 12, booked: 9, myReservation: false },
            { id: 6, name: 'Open Box Friday', day: 'Viernes', time: '17:00', trainer: 'Esteban Guevara', max: 20, booked: 14, myReservation: true },
            { id: 7, name: 'Weekend Warrior', day: 'Sábado', time: '08:00', trainer: 'Esteban Guevara', max: 20, booked: 6, myReservation: false },
        ],

        notifications: [
            { id: 1, type: 'payment', msg: 'Pago de ₡25,000 registrado para Keren Mora — Membresía Mensual', time: 'Hace 2 min', read: false },
            { id: 2, type: 'expiry', msg: 'Membresía de Juan Daniel Ávila vence en 3 días', time: 'Hace 1 hora', read: false },
            { id: 3, type: 'wod', msg: 'Nuevo WOD publicado: "AMRAP 20 — Fuego y Acero" — 18 Abr', time: 'Hace 3 horas', read: false },
            { id: 4, type: 'reservation', msg: 'Confirmación de reserva: CrossFit Intensivo — Martes 07:00', time: 'Ayer 18:30', read: false },
            { id: 5, type: 'expiry', msg: 'Membresía de Sofía Castro vence en 5 días — Trimestral', time: 'Ayer 12:00', read: true },
            { id: 6, type: 'payment', msg: 'Pago confirmado de Andrea Solís — ₡25,000 SINPE', time: 'Ayer 11:30', read: true },
            { id: 7, type: 'wod', msg: 'WOD "For Time — Murph" publicado para el 17 Abr', time: 'Hace 2 días', read: true },
            { id: 8, type: 'reservation', msg: 'Nueva reserva confirmada: Open Box Friday — Viernes 17:00', time: 'Hace 2 días', read: true },
        ],

        inventory: [
            { id: 1, name: 'Barras Olímpicas 20kg', category: 'Pesas y Barras', qty: 8, minStock: 5, status: 'Activo', acquired: '2023-01-15', cost: '₡180,000', lastMaint: '2025-02-01', desc: 'Barras olímpicas estándar certificadas' },
            { id: 2, name: 'Discos de Goma 20kg', category: 'Pesas y Barras', qty: 20, minStock: 10, status: 'Activo', acquired: '2023-01-15', cost: '₡45,000', lastMaint: '—', desc: 'Discos de goma para protección del piso' },
            { id: 3, name: 'Remo Concept2 Model D', category: 'Cardio', qty: 3, minStock: 2, status: 'Activo', acquired: '2023-06-10', cost: '₡950,000', lastMaint: '2025-03-15', desc: 'Remos de alta performance para crossfit' },
            { id: 4, name: 'Aros de Gimnasia', category: 'Accesorios', qty: 10, minStock: 6, status: 'Activo', acquired: '2023-03-20', cost: '₡35,000', lastMaint: '—', desc: 'Anillos de madera para movimientos olímpicos' },
            { id: 5, name: 'Kettlebells 24kg', category: 'Pesas y Barras', qty: 2, minStock: 4, status: 'Activo', acquired: '2023-04-05', cost: '₡75,000', lastMaint: '—', desc: 'Kettlebells de hierro fundido — STOCK BAJO' },
            { id: 6, name: 'Bicicleta Assault', category: 'Cardio', qty: 2, minStock: 1, status: 'Mantenimiento', acquired: '2022-11-10', cost: '₡1,200,000', 'lastMaint': '2025-04-10', desc: 'Air bike para entrenamiento HIIT' },
            { id: 7, name: 'Cajones de Salto (set)', category: 'Accesorios', qty: 6, minStock: 3, status: 'Activo', acquired: '2023-07-20', cost: '₡85,000', lastMaint: '2024-12-01', desc: '3 alturas: 40, 50, 60cm' },
            { id: 8, name: 'Proteína Whey 2kg', category: 'Suplementos', qty: 1, minStock: 3, status: 'Activo', acquired: '2025-03-01', cost: '₡35,000', lastMaint: '—', desc: 'Stock en tienda — STOCK BAJO' },
            { id: 9, name: 'Colchonetas Olímpicas', category: 'Equipamiento', qty: 12, minStock: 8, status: 'Activo', acquired: '2023-02-10', cost: '₡28,000', lastMaint: '—', desc: 'Colchonetas para estiramientos y yoga' },
            { id: 10, 'name': 'Cuerda de Salto Speed', category: 'Accesorios', qty: 15, minStock: 8, status: 'Inactivo', acquired: '2023-08-15', cost: '₡8,500', lastMaint: '—', desc: 'Cuerdas speed para double unders' },
        ],

        nextId: { clients: 11, memberships: 11, payments: 8, wods: 5, classes: 8, notifications: 9, inventory: 11 },

        confirmCallback: null,
    };

    return {
        get: (key) => state[key],
        set: (key, value) => { state[key] = value; },
        getClient: (id) => state.clients.find(c => c.id === id),
        getNextId: (entity) => state.nextId[entity]++,
    };
})();

// ══════════════════════════════════════════════════════════════════════
// AUTH SERVICE (Single Responsibility)
// ══════════════════════════════════════════════════════════════════════
const AuthService = {
    users: {
        admin: { name: 'Keren Mora Quesada', role: 'Administrador', avatar: 'KM', sections: ['dashboard', 'clients', 'memberships', 'payments', 'wod', 'reservations', 'notifications', 'inventory', 'reports'] },
        receptionist: { name: 'Andrea Solís', role: 'Recepcionista', avatar: 'AS', sections: ['clients', 'memberships', 'payments', 'notifications'] },
        trainer: { name: 'Esteban Guevara', role: 'Entrenador', avatar: 'EG', sections: ['clients', 'wod', 'reservations', 'inventory', 'notifications'] },
    },
    login(roleKey) {
        const user = this.users[roleKey];
        if (!user) return false;
        Store.set('currentUser', { ...user, roleKey });
        return true;
    },
    logout() { Store.set('currentUser', null); },
    hasAccess(section) {
        const user = Store.get('currentUser');
        return user && user.sections.includes(section);
    },
};

// ══════════════════════════════════════════════════════════════════════
// UI SERVICE — Toast, Modals (Single Responsibility)
// ══════════════════════════════════════════════════════════════════════
const UIService = {
    _toastTimer: null,
    showToast(message, type = 'success') {
        const toast = document.getElementById('toast');
        const icon = document.getElementById('toastIcon');
        const msg = document.getElementById('toastMessage');
        const icons = { success: 'check_circle', error: 'error', warning: 'warning', info: 'info' };
        const colors = { success: '#4ade80', error: '#ffb4ab', warning: '#fbbf24', info: '#ac79f2' };
        icon.textContent = icons[type] || 'info';
        icon.style.color = colors[type] || '#ac79f2';
        msg.textContent = message;
        toast.classList.add('show');
        clearTimeout(this._toastTimer);
        this._toastTimer = setTimeout(() => toast.classList.remove('show'), 3000);
    },
    openModal(id) { document.getElementById(id).classList.add('show'); },
    closeModal(id) { document.getElementById(id).classList.remove('show'); },
    openConfirm(title, message, callback) {
        Store.set('confirmCallback', callback);
        document.getElementById('confirmTitle').textContent = title;
        document.getElementById('confirmMessage').textContent = message;
        document.getElementById('confirmModal').classList.add('show');
    },
    closeConfirm() {
        document.getElementById('confirmModal').classList.remove('show');
        Store.set('confirmCallback', null);
    },
    executeConfirm() {
        const cb = Store.get('confirmCallback');
        if (cb) cb();
        this.closeConfirm();
    },
};

// ══════════════════════════════════════════════════════════════════════
// RENDERER — Pure render functions (Open/Closed Principle)
// ══════════════════════════════════════════════════════════════════════
const Renderer = {
    // Badge helpers
    statusBadge(status) {
        const map = {
            'Activa': '<span class="badge-active inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Activa</span>',
            'Vencida': '<span class="badge-expired inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Vencida</span>',
            'Suspendida': '<span class="badge-suspended inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Suspendida</span>',
            'Mantenimiento': '<span class="badge-maintenance inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Mantenimiento</span>',
            'Inactivo': '<span class="badge-inactive inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Inactivo</span>',
            'Activo': '<span class="badge-active inline-flex items-center px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider">Activo</span>',
        };
        return map[status] || `<span class="text-outline text-xs">${status}</span>`;
    },

    avatarHtml(avatar, status) {
        const color = status === 'Vencida' ? 'bg-error-container/30 text-error' : status === 'Suspendida' ? 'bg-amber-900/20 text-amber-400' : 'bg-primary-container/20 text-primary-container';
        return `<div class="w-8 h-8 rounded-full ${color} flex items-center justify-center font-headline font-bold text-xs flex-shrink-0">${avatar}</div>`;
    },

    actionBtns(editFn, deleteFn) {
        return `<div class="flex items-center justify-end gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
      <button class="btn-icon edit" onclick="${editFn}" title="Editar"><span class="material-symbols-outlined text-[18px]" style="font-variation-settings:'wght' 300">edit</span></button>
      <button class="btn-icon delete" onclick="${deleteFn}" title="Eliminar"><span class="material-symbols-outlined text-[18px]" style="font-variation-settings:'wght' 300">delete</span></button>
    </div>`;
    },

    // ── Clients Table ──────────────────────────────────────────────────
    renderClients(filter = '', statusFilter = '') {
        const clients = Store.get('clients').filter(c => {
            const q = filter.toLowerCase();
            const matchSearch = !q || c.name.toLowerCase().includes(q) || c.cedula.includes(q) || c.email.toLowerCase().includes(q);
            const matchStatus = !statusFilter || c.status === statusFilter;
            return matchSearch && matchStatus;
        });

        const tbody = document.getElementById('clientsTableBody');
        if (!clients.length) {
            tbody.innerHTML = `<tr><td colspan="6" class="text-center py-12 text-outline">
        <span class="material-symbols-outlined text-[40px] block mb-2" style="font-variation-settings:'wght' 200">person_search</span>
        No se encontraron clientes
      </td></tr>`;
            document.getElementById('clientsCount').textContent = 'Sin resultados';
            return;
        }

        tbody.innerHTML = clients.map(c => `
      <tr class="group">
        <td><div class="flex items-center gap-3">${this.avatarHtml(c.avatar, c.status)}<div><p class="text-on-surface font-medium text-sm">${c.name}</p><p class="text-outline text-xs">${c.address}</p></div></div></td>
        <td class="text-on-surface-variant text-sm">${c.cedula}</td>
        <td><p class="text-on-surface-variant text-sm">${c.email}</p><p class="text-outline text-xs mt-0.5">${c.phone}</p></td>
        <td>${this.statusBadge(c.status)}</td>
        <td class="text-sm ${c.status === 'Vencida' ? 'text-error font-semibold' : 'text-on-surface-variant'}">${formatDate(c.expiry)}</td>
        <td class="text-right">${this.actionBtns(`editClient(${c.id})`, `confirmDeleteClient(${c.id})`)}</td>
      </tr>
    `).join('');

        document.getElementById('clientsCount').textContent = `Mostrando ${clients.length} de ${Store.get('clients').length} clientes`;
    },

    // ── Memberships Table ─────────────────────────────────────────────
    renderMemberships() {
        const memberships = Store.get('memberships');
        const clients = Store.get('clients');
        const tbody = document.getElementById('membershipsTableBody');

        if (!memberships.length) {
            tbody.innerHTML = `<tr><td colspan="7" class="text-center py-12 text-outline">No hay membresías registradas</td></tr>`;
            return;
        }

        tbody.innerHTML = memberships.map(m => {
            const client = clients.find(c => c.id === m.clientId);
            const clientName = client ? client.name : 'Cliente eliminado';
            const avatar = client ? client.avatar : '??';
            return `
        <tr class="group">
          <td><div class="flex items-center gap-3">${this.avatarHtml(avatar, m.status)}<span class="text-on-surface font-medium text-sm">${clientName}</span></div></td>
          <td><span class="text-on-surface-variant text-sm font-medium">${m.plan}</span></td>
          <td class="text-on-surface-variant text-sm">${formatDate(m.start)}</td>
          <td class="text-sm ${m.status === 'Vencida' ? 'text-error font-semibold' : 'text-on-surface-variant'}">${formatDate(m.end)}</td>
          <td class="text-primary font-semibold text-sm">${m.amount}</td>
          <td>${this.statusBadge(m.status)}</td>
          <td class="text-right">${this.actionBtns(`editMembership(${m.id})`, `confirmDeleteMembership(${m.id})`)}</td>
        </tr>
      `;
        }).join('');
    },

    // ── Payments Table ────────────────────────────────────────────────
    renderPayments() {
        const payments = Store.get('payments');
        const clients = Store.get('clients');
        const tbody = document.getElementById('paymentsTableBody');

        const methodIcon = { 'Efectivo': 'payments', 'SINPE': 'smartphone', 'Tarjeta': 'credit_card' };
        const methodColor = { 'Efectivo': 'text-tertiary-fixed', 'SINPE': 'text-primary-container', 'Tarjeta': 'text-secondary' };

        tbody.innerHTML = payments.map(p => {
            const client = clients.find(c => c.id === p.clientId);
            const cn = client ? client.name : 'Cliente';
            const av = client ? client.avatar : '??';
            return `
        <tr class="group">
          <td><div class="flex items-center gap-3">${this.avatarHtml(av, 'Activa')}<span class="text-on-surface font-medium text-sm">${cn}</span></div></td>
          <td class="text-on-surface-variant text-sm">${formatDate(p.date)}</td>
          <td class="text-primary font-bold font-headline text-base">${p.amount}</td>
          <td><div class="flex items-center gap-1.5 ${methodColor[p.method] || 'text-on-surface-variant'}"><span class="material-symbols-outlined text-[16px]" style="font-variation-settings:'wght' 300">${methodIcon[p.method] || 'payment'}</span><span class="text-sm font-medium">${p.method}</span></div></td>
          <td class="text-on-surface-variant text-sm">${p.plan}</td>
          <td class="text-right"><div class="flex items-center justify-end gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
            <button class="btn-icon" onclick="showReceipt(${p.id})" title="Ver comprobante"><span class="material-symbols-outlined text-[18px]" style="font-variation-settings:'wght' 300">receipt</span></button>
            <button class="btn-icon delete" onclick="confirmDeletePayment(${p.id})" title="Eliminar"><span class="material-symbols-outlined text-[18px]" style="font-variation-settings:'wght' 300">delete</span></button>
          </div></td>
        </tr>
      `;
        }).join('');
    },

    renderDelinquent() {
        const expired = Store.get('clients').filter(c => c.status === 'Vencida');
        const tbody = document.getElementById('delinquentTableBody');
        tbody.innerHTML = expired.map(c => {
            const daysDiff = Math.floor((new Date() - new Date(c.expiry)) / (1000 * 60 * 60 * 24));
            return `
        <tr class="group">
          <td><div class="flex items-center gap-3">${this.avatarHtml(c.avatar, 'Vencida')}<div><p class="text-on-surface font-medium text-sm">${c.name}</p><p class="text-outline text-xs">${c.email}</p></div></div></td>
          <td>${this.statusBadge('Vencida')}</td>
          <td><span class="text-error font-bold text-sm">${daysDiff > 0 ? daysDiff : 0} días</span></td>
          <td class="text-on-surface-variant text-sm">${formatDate(c.expiry)}</td>
          <td class="text-right"><button class="btn-primary py-1.5 px-3 text-xs" onclick="openMembershipRenewal(${c.id})"><span class="material-symbols-outlined text-[14px]">autorenew</span> Renovar</button></td>
        </tr>
      `;
        }).join('');
    },

    // ── WOD Grid ──────────────────────────────────────────────────────
    renderWODs() {
        const wods = Store.get('wods');
        const container = document.getElementById('wodGrid');
        const canEdit = Store.get('currentUser')?.roleKey !== 'receptionist';

        if (!wods.length) {
            container.innerHTML = `<div class="col-span-3 text-center py-16 text-outline">
        <span class="material-symbols-outlined text-[48px] block mb-3" style="font-variation-settings:'wght' 200">fitness_center</span>
        No hay WODs publicados. ¡Publica el primero!
      </div>`;
            return;
        }

        container.innerHTML = wods.map(w => `
      <div class="wod-card">
        <div class="flex items-start justify-between mb-3">
          <div>
            <span class="text-[10px] font-bold uppercase tracking-wider text-primary-container bg-primary-container/10 px-2 py-0.5 rounded-md">${w.category}</span>
            <h3 class="font-headline font-bold text-base mt-2 leading-tight">${w.name}</h3>
            <p class="text-outline text-xs mt-1">${formatDate(w.date)}</p>
          </div>
          ${canEdit ? `<div class="flex gap-1 flex-shrink-0">
            <button class="btn-icon edit" onclick="editWOD(${w.id})"><span class="material-symbols-outlined text-[16px]" style="font-variation-settings:'wght' 300">edit</span></button>
            <button class="btn-icon delete" onclick="confirmDeleteWOD(${w.id})"><span class="material-symbols-outlined text-[16px]" style="font-variation-settings:'wght' 300">delete</span></button>
          </div>` : ''}
        </div>
        ${w.desc ? `<p class="text-on-surface-variant text-xs mb-4 italic">${w.desc}</p>` : ''}
        <div class="space-y-2">
          ${w.exercises.map(e => `
            <div class="flex items-center gap-2 bg-surface-container-high rounded-xl px-3 py-2">
              <span class="material-symbols-outlined text-primary-container text-[16px]" style="font-variation-settings:'wght' 300">radio_button_checked</span>
              <span class="text-on-surface font-medium text-sm flex-1">${e.name}</span>
              <span class="text-outline text-xs">${e.sets} × ${e.reps}${e.weight ? ' @ ' + e.weight : ''}</span>
            </div>
          `).join('')}
        </div>
      </div>
    `).join('');
    },

    // ── Reservations ─────────────────────────────────────────────────
    renderReservations() {
        const classes = Store.get('classes');
        const container = document.getElementById('reservationsGrid');
        const myRes = document.getElementById('myReservations');

        container.innerHTML = classes.map(cls => {
            const full = cls.booked >= cls.max;
            const slotClass = cls.myReservation ? 'mine' : (full ? 'full' : 'available');
            const pct = Math.round((cls.booked / cls.max) * 100);
            return `
        <div class="reservation-slot ${slotClass}" onclick="${!full || cls.myReservation ? `toggleReservation(${cls.id})` : ''}">
          <div class="flex items-start justify-between mb-2">
            <div>
              <p class="font-headline font-bold text-sm">${cls.name}</p>
              <p class="text-outline text-xs">${cls.day} · ${cls.time}</p>
            </div>
            <div class="text-right">
              ${cls.myReservation
                    ? '<span class="badge-active inline-flex items-center px-1.5 py-0.5 rounded text-[9px] font-bold uppercase">Reservado</span>'
                    : full
                        ? '<span class="badge-expired inline-flex items-center px-1.5 py-0.5 rounded text-[9px] font-bold uppercase">Lleno</span>'
                        : '<span class="badge-active inline-flex items-center px-1.5 py-0.5 rounded text-[9px] font-bold uppercase">Disponible</span>'
                }
            </div>
          </div>
          <div class="flex items-center gap-2 mb-2">
            <span class="material-symbols-outlined text-outline text-[14px]" style="font-variation-settings:'wght' 300">person</span>
            <span class="text-outline text-xs">${cls.trainer}</span>
          </div>
          <div class="flex items-center gap-2">
            <div class="progress-bar-bg flex-1" style="height:4px">
              <div class="progress-bar-fill" style="width:${pct}%"></div>
            </div>
            <span class="text-outline text-xs flex-shrink-0">${cls.booked}/${cls.max}</span>
          </div>
        </div>
      `;
        }).join('');

        const myClasses = classes.filter(c => c.myReservation);
        if (!myClasses.length) {
            myRes.innerHTML = `<p class="text-outline text-sm">No tienes reservas activas.</p>`;
        } else {
            myRes.innerHTML = myClasses.map(cls => `
        <div class="flex items-center justify-between bg-surface-container-high rounded-xl px-4 py-3">
          <div>
            <p class="font-headline font-semibold text-sm">${cls.name}</p>
            <p class="text-outline text-xs">${cls.day} · ${cls.time} · ${cls.trainer}</p>
          </div>
          <button class="btn-danger text-xs py-1.5 px-3" onclick="toggleReservation(${cls.id})">Cancelar</button>
        </div>
      `).join('');
        }
    },

    // ── Notifications ─────────────────────────────────────────────────
    renderNotifications() {
        const notifs = Store.get('notifications');
        const container = document.getElementById('notificationsList');
        const icons = { payment: 'payments', expiry: 'warning', wod: 'fitness_center', reservation: 'calendar_today' };
        const colors = { payment: 'text-tertiary-fixed', expiry: 'text-[#fbbf24]', wod: 'text-primary-container', reservation: 'text-secondary' };

        container.innerHTML = notifs.map(n => `
      <div class="notification-item ${n.read ? '' : 'unread'}" onclick="markNotifRead(${n.id})">
        <div class="flex items-start gap-3">
          <span class="material-symbols-outlined ${colors[n.type] || 'text-primary'} text-[22px] flex-shrink-0 mt-0.5" style="font-variation-settings:'wght' 300">${icons[n.type] || 'notifications'}</span>
          <div class="flex-1 min-w-0">
            <p class="text-sm ${n.read ? 'text-on-surface-variant' : 'text-on-surface font-medium'}">${n.msg}</p>
            <p class="text-outline text-xs mt-1">${n.time}</p>
          </div>
          ${!n.read ? '<div class="w-2 h-2 bg-primary-container rounded-full flex-shrink-0 mt-1.5"></div>' : ''}
        </div>
      </div>
    `).join('');

        const unreadCount = notifs.filter(n => !n.read).length;
        document.getElementById('notifBadge').textContent = unreadCount;
        document.getElementById('notifBadgeSidebar').textContent = unreadCount;
        if (!unreadCount) {
            document.getElementById('notifBadge').style.display = 'none';
        } else {
            document.getElementById('notifBadge').style.display = 'flex';
        }
    },

    // ── Inventory Table ───────────────────────────────────────────────
    renderInventory(filter = '', statusFilter = '') {
        const items = Store.get('inventory').filter(i => {
            const q = filter.toLowerCase();
            const matchSearch = !q || i.name.toLowerCase().includes(q) || i.category.toLowerCase().includes(q);
            const matchStatus = !statusFilter || i.status === statusFilter;
            return matchSearch && matchStatus;
        });

        const tbody = document.getElementById('inventoryTableBody');

        tbody.innerHTML = items.map(i => {
            const lowStock = i.qty <= i.minStock;
            return `
        <tr class="group ${lowStock ? 'inventory-row-critical' : ''}">
          <td><div>
            <p class="text-on-surface font-medium text-sm">${i.name} ${lowStock ? '<span class="text-[#fbbf24] text-[10px] font-bold ml-1">⚠ STOCK BAJO</span>' : ''}</p>
            <p class="text-outline text-xs">${i.desc}</p>
          </div></td>
          <td><span class="text-on-surface-variant text-xs bg-surface-container-high px-2 py-0.5 rounded-md">${i.category}</span></td>
          <td><span class="font-headline font-bold ${lowStock ? 'text-[#fbbf24]' : 'text-on-surface'} text-base">${i.qty}</span><span class="text-outline text-xs ml-1">/ min ${i.minStock}</span></td>
          <td>${this.statusBadge(i.status)}</td>
          <td class="text-on-surface-variant text-sm">${formatDate(i.acquired)}</td>
          <td class="text-on-surface-variant text-sm">${i.lastMaint === '—' ? '<span class="text-outline">Sin mantenimiento</span>' : i.lastMaint}</td>
          <td class="text-right">${this.actionBtns(`editInventory(${i.id})`, `confirmDeleteInventory(${i.id})`)}</td>
        </tr>
      `;
        }).join('');
    },

    // ── Dashboard ─────────────────────────────────────────────────────
    renderDashboard() {
        // Chart
        const days = ['Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb', 'Dom'];
        const values = [28, 34, 41, 31, 45, 52, 18];
        const maxV = Math.max(...values);
        const chart = document.getElementById('attendanceChart');
        const labels = document.getElementById('chartLabels');
        chart.innerHTML = values.map((v, i) => {
            const h = Math.round((v / maxV) * 100);
            return `<div class="flex flex-col items-center gap-1 flex-1 group cursor-pointer" onclick="">
        <span class="text-xs font-headline font-bold text-on-surface-variant opacity-0 group-hover:opacity-100 transition-opacity">${v}</span>
        <div class="chart-bar w-full" style="height:${h}%" title="${days[i]}: ${v} atletas"></div>
      </div>`;
        }).join('');
        labels.innerHTML = days.map(d => `<span class="flex-1 text-center">${d}</span>`).join('');

        // Expiring soon (next 7 days)
        const soon = Store.get('clients').filter(c => {
            if (c.status !== 'Activa') return false;
            const days = Math.ceil((new Date(c.expiry) - new Date()) / (1000 * 60 * 60 * 24));
            return days <= 7 && days >= 0;
        }).slice(0, 5);

        document.getElementById('expiringList').innerHTML = soon.length
            ? soon.map(c => {
                const daysLeft = Math.ceil((new Date(c.expiry) - new Date()) / (1000 * 60 * 60 * 24));
                return `<div class="flex items-center gap-3 cursor-pointer hover:bg-surface-container-high rounded-xl px-2 py-1.5 transition-colors -mx-2" onclick="navigateTo('clients')">
            ${this.avatarHtml(c.avatar, 'Activa')}
            <div class="flex-1 min-w-0">
              <p class="text-on-surface font-medium text-xs truncate">${c.name}</p>
              <p class="text-outline text-[10px]">${c.plan}</p>
            </div>
            <span class="text-[#fbbf24] text-xs font-bold flex-shrink-0">${daysLeft}d</span>
          </div>`;
            }).join('')
            : '<p class="text-outline text-xs text-center py-4">Sin vencimientos próximos</p>';

        // Ranking
        const ranking = [
            { name: 'Oliver Román Q.', avatar: 'OR', count: 22 },
            { name: 'Keren Mora Q.', avatar: 'KM', count: 18 },
            { name: 'Pamela Sibaja T.', avatar: 'PS', count: 16 },
            { name: 'Andrea Solís B.', avatar: 'AS', count: 14 },
            { name: 'Diana Vega P.', avatar: 'DV', count: 11 },
        ];
        document.getElementById('rankingList').innerHTML = ranking.map((r, i) => `
      <div class="flex items-center gap-3">
        <div class="rank-badge rank-${i < 3 ? i + 1 : 'other'}">${i + 1}</div>
        ${this.avatarHtml(r.avatar, 'Activa')}
        <span class="flex-1 text-on-surface text-sm font-medium">${r.name}</span>
        <span class="text-primary font-headline font-bold text-sm">${r.count} asist.</span>
      </div>
    `).join('');

        // Today's income
        const today = new Date().toISOString().slice(0, 10);
        document.getElementById('todayDate').textContent = new Date().toLocaleDateString('es-CR', { weekday: 'long', day: 'numeric', month: 'long' });

        const todayPayments = Store.get('payments').slice(-3);
        const clients = Store.get('clients');
        document.getElementById('todayPaymentsList').innerHTML = todayPayments.map(p => {
            const c = clients.find(cl => cl.id === p.clientId);
            return `<div class="flex items-center justify-between py-1.5">
        <span class="text-on-surface-variant text-xs">${c ? c.name : 'Cliente'}</span>
        <span class="text-primary font-bold text-sm font-headline">${p.amount}</span>
      </div>`;
        }).join('');

        // Update metric cards
        const activeClients = Store.get('clients').filter(c => c.status === 'Activa').length;
        document.getElementById('metricClients').textContent = activeClients;
    },

    // ── Reports Table ─────────────────────────────────────────────────
    renderReports() {
        const payments = Store.get('payments');
        const clients = Store.get('clients');
        const tbody = document.getElementById('reportTableBody');
        tbody.innerHTML = payments.map(p => {
            const c = clients.find(cl => cl.id === p.clientId);
            return `<tr>
        <td class="text-on-surface font-medium text-sm">${c ? c.name : '—'}</td>
        <td class="text-on-surface-variant text-sm">${formatDate(p.date)}</td>
        <td class="text-on-surface-variant text-sm">${p.plan}</td>
        <td class="text-on-surface-variant text-sm">${p.method}</td>
        <td class="text-right text-primary font-bold font-headline">${p.amount}</td>
      </tr>`;
        }).join('');
    },
};

// ══════════════════════════════════════════════════════════════════════
// NAVIGATION (Single Responsibility)
// ══════════════════════════════════════════════════════════════════════
function navigateTo(section) {
    const user = Store.get('currentUser');
    if (!user) return;
    if (!AuthService.hasAccess(section)) {
        UIService.showToast('No tienes permiso para acceder a esta sección', 'error');
        return;
    }

    Store.set('currentSection', section);

    // Update sidebar
    document.querySelectorAll('.nav-item').forEach(el => {
        el.classList.toggle('active', el.dataset.section === section);
    });

    // Show/hide pages
    document.querySelectorAll('.section-page').forEach(p => p.classList.remove('active'));
    const page = document.getElementById(`page-${section}`);
    if (page) page.classList.add('active');

    // Render section
    const renderMap = {
        dashboard: () => Renderer.renderDashboard(),
        clients: () => Renderer.renderClients(),
        memberships: () => Renderer.renderMemberships(),
        payments: () => { Renderer.renderPayments(); Renderer.renderDelinquent(); },
        wod: () => Renderer.renderWODs(),
        reservations: () => Renderer.renderReservations(),
        notifications: () => Renderer.renderNotifications(),
        inventory: () => Renderer.renderInventory(),
        reports: () => Renderer.renderReports(),
    };
    if (renderMap[section]) renderMap[section]();

    // Role-based UI adjustments
    applyRolePermissions(user);
}

function applyRolePermissions(user) {
    const btnPublishWOD = document.getElementById('btnPublishWOD');
    const btnNewClass = document.getElementById('btnNewClass');
    if (btnPublishWOD) btnPublishWOD.style.display = (user.roleKey === 'receptionist') ? 'none' : '';
    if (btnNewClass) btnNewClass.style.display = (user.roleKey === 'trainer') ? 'none' : '';
}

// ══════════════════════════════════════════════════════════════════════
// AUTH HANDLERS
// ══════════════════════════════════════════════════════════════════════


// ══════════════════════════════════════════════════════════════════════
// CLIENT CRUD
// ══════════════════════════════════════════════════════════════════════
function openClientModal(id = null) {
    const form = document.getElementById('clientForm');
    form.reset();
    document.getElementById('clientDate').value = new Date().toISOString().slice(0, 10);
    document.getElementById('clientId').value = '';

    if (id) {
        const c = Store.getClient(id);
        if (!c) return;
        document.getElementById('clientModalTitle').textContent = 'Editar Cliente';
        document.getElementById('clientId').value = c.id;
        document.getElementById('clientName').value = c.name;
        document.getElementById('clientCedula').value = c.cedula;
        document.getElementById('clientPhone').value = c.phone;
        document.getElementById('clientEmail').value = c.email;
        document.getElementById('clientAddress').value = c.address;
        document.getElementById('clientDate').value = c.date;
        document.getElementById('clientStatus').value = c.status;
    } else {
        document.getElementById('clientModalTitle').textContent = 'Nuevo Cliente';
    }
    UIService.openModal('clientModal');
}

function editClient(id) { openClientModal(id); }

document.getElementById('clientForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const id = document.getElementById('clientId').value;
    const data = {
        name: document.getElementById('clientName').value.trim(),
        cedula: document.getElementById('clientCedula').value.trim(),
        phone: document.getElementById('clientPhone').value.trim(),
        email: document.getElementById('clientEmail').value.trim(),
        address: document.getElementById('clientAddress').value.trim(),
        date: document.getElementById('clientDate').value,
        status: document.getElementById('clientStatus').value,
        expiry: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toISOString().slice(0, 10),
        plan: 'Mensual',
    };

    const clients = Store.get('clients');
    if (id) {
        const idx = clients.findIndex(c => c.id === parseInt(id));
        if (idx !== -1) { clients[idx] = { ...clients[idx], ...data }; }
        UIService.showToast('Cliente actualizado correctamente', 'success');
    } else {
        // Check duplicate cedula
        if (clients.find(c => c.cedula === data.cedula)) {
            UIService.showToast('Ya existe un cliente con esa cédula', 'error');
            return;
        }
        data.id = Store.getNextId('clients');
        data.avatar = data.name.split(' ').map(w => w[0]).slice(0, 2).join('').toUpperCase();
        clients.push(data);
        addNotification('Nuevo cliente registrado: ' + data.name, 'payment');
        UIService.showToast('Cliente registrado exitosamente', 'success');
    }
    Store.set('clients', clients);
    UIService.closeModal('clientModal');
    Renderer.renderClients();
    populateClientSelects();
});

function confirmDeleteClient(id) {
    UIService.openConfirm('Eliminar Cliente', '¿Estás seguro de eliminar este cliente? Esta acción no se puede deshacer.', () => deleteClient(id));
}

function deleteClient(id) {
    Store.set('clients', Store.get('clients').filter(c => c.id !== id));
    Renderer.renderClients();
    UIService.showToast('Cliente eliminado', 'info');
}

function filterClients() {
    const search = document.getElementById('clientSearch').value;
    const status = document.getElementById('clientStatusFilter').value;
    Renderer.renderClients(search, status);
}

// ══════════════════════════════════════════════════════════════════════
// MEMBERSHIP CRUD
// ══════════════════════════════════════════════════════════════════════
function populateClientSelects() {
    const clients = Store.get('clients');
    const opts = clients.map(c => `<option value="${c.id}">${c.name}</option>`).join('');
    ['membershipClient', 'paymentClient'].forEach(id => {
        const el = document.getElementById(id);
        if (el) el.innerHTML = opts;
    });
}

function openMembershipModal(id = null) {
    populateClientSelects();
    const form = document.getElementById('membershipForm');
    form.reset();
    document.getElementById('membershipId').value = '';
    document.getElementById('membershipStart').value = new Date().toISOString().slice(0, 10);
    const endDate = new Date(); endDate.setMonth(endDate.getMonth() + 1);
    document.getElementById('membershipEnd').value = endDate.toISOString().slice(0, 10);

    if (id) {
        const m = Store.get('memberships').find(m => m.id === id);
        if (!m) return;
        document.getElementById('membershipModalTitle').textContent = 'Editar Membresía';
        document.getElementById('membershipId').value = m.id;
        document.getElementById('membershipClient').value = m.clientId;
        document.getElementById('membershipPlan').value = m.plan + ' — ' + m.amount;
        document.getElementById('membershipStatus').value = m.status;
        document.getElementById('membershipStart').value = m.start;
        document.getElementById('membershipEnd').value = m.end;
        document.getElementById('membershipAmount').value = m.amount;
    } else {
        document.getElementById('membershipModalTitle').textContent = 'Nueva Membresía';
    }
    UIService.openModal('membershipModal');
}

function editMembership(id) { openMembershipModal(id); }

function openMembershipRenewal(clientId) {
    populateClientSelects();
    document.getElementById('membershipId').value = '';
    document.getElementById('membershipClient').value = clientId;
    document.getElementById('membershipModalTitle').textContent = 'Renovar Membresía';
    document.getElementById('membershipStatus').value = 'Activa';
    document.getElementById('membershipStart').value = new Date().toISOString().slice(0, 10);
    const end = new Date(); end.setMonth(end.getMonth() + 1);
    document.getElementById('membershipEnd').value = end.toISOString().slice(0, 10);
    UIService.openModal('membershipModal');
}

document.getElementById('membershipForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const id = document.getElementById('membershipId').value;
    const planVal = document.getElementById('membershipPlan').value.split(' — ')[0];
    const data = {
        clientId: parseInt(document.getElementById('membershipClient').value),
        plan: planVal,
        start: document.getElementById('membershipStart').value,
        end: document.getElementById('membershipEnd').value,
        amount: document.getElementById('membershipAmount').value || getPlanPrice(planVal),
        status: document.getElementById('membershipStatus').value,
    };

    const memberships = Store.get('memberships');
    if (id) {
        const idx = memberships.findIndex(m => m.id === parseInt(id));
        if (idx !== -1) memberships[idx] = { ...memberships[idx], ...data };
        UIService.showToast('Membresía actualizada', 'success');
    } else {
        data.id = Store.getNextId('memberships');
        memberships.push(data);
        // Update client status
        const clients = Store.get('clients');
        const ci = clients.findIndex(c => c.id === data.clientId);
        if (ci !== -1) { clients[ci].status = data.status; clients[ci].expiry = data.end; clients[ci].plan = data.plan; }
        Store.set('clients', clients);
        addNotification('Nueva membresía asignada', 'payment');
        UIService.showToast('Membresía creada exitosamente', 'success');
    }
    Store.set('memberships', memberships);
    UIService.closeModal('membershipModal');
    Renderer.renderMemberships();
});

function confirmDeleteMembership(id) {
    UIService.openConfirm('Eliminar Membresía', '¿Eliminar esta membresía?', () => {
        Store.set('memberships', Store.get('memberships').filter(m => m.id !== id));
        Renderer.renderMemberships();
        UIService.showToast('Membresía eliminada', 'info');
    });
}

// ══════════════════════════════════════════════════════════════════════
// PAYMENT CRUD
// ══════════════════════════════════════════════════════════════════════
function openPaymentModal() {
    populateClientSelects();
    document.getElementById('paymentForm').reset();
    document.getElementById('paymentDate').value = new Date().toISOString().slice(0, 10);
    UIService.openModal('paymentModal');
}

document.getElementById('paymentForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const data = {
        id: Store.getNextId('payments'),
        clientId: parseInt(document.getElementById('paymentClient').value),
        date: document.getElementById('paymentDate').value,
        amount: document.getElementById('paymentAmount').value,
        method: document.getElementById('paymentMethod').value,
        plan: document.getElementById('paymentPlan').value,
    };
    const payments = Store.get('payments');
    payments.push(data);
    Store.set('payments', payments);
    addNotification('Pago registrado — ' + data.amount, 'payment');
    UIService.closeModal('paymentModal');
    Renderer.renderPayments();
    UIService.showToast('Pago registrado correctamente', 'success');
});

function confirmDeletePayment(id) {
    UIService.openConfirm('Eliminar Pago', '¿Eliminar este registro de pago?', () => {
        Store.set('payments', Store.get('payments').filter(p => p.id !== id));
        Renderer.renderPayments();
        UIService.showToast('Pago eliminado', 'info');
    });
}

function showPaymentsTab(tab, btn) {
    document.querySelectorAll('#page-payments .tab-btn').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    document.getElementById('paymentsAll').classList.toggle('hidden', tab !== 'all');
    document.getElementById('paymentsDelinquent').classList.toggle('hidden', tab !== 'delinquent');
}

function showReceipt(id) {
    const p = Store.get('payments').find(pay => pay.id === id);
    if (!p) return;
    const c = Store.getClient(p.clientId);
    document.getElementById('receiptContent').innerHTML = `
    <div class="text-center mb-4">
      <h4 class="font-headline font-black text-xl text-primary-container italic">Orion Fit Studio</h4>
      <p class="text-outline text-xs">Comprobante de Pago</p>
    </div>
    <div class="space-y-2 text-sm">
      ${[['Cliente:', c ? c.name : '—'], ['Cédula:', c ? c.cedula : '—'], ['Fecha:', formatDate(p.date)],
        ['Plan:', p.plan], ['Método:', p.method], ['Monto:', p.amount]].map(([k, v]) => `
        <div class="flex justify-between py-2 border-b border-outline-variant/10">
          <span class="text-outline">${k}</span><span class="text-on-surface font-semibold">${v}</span>
        </div>`).join('')}
    </div>
    <p class="text-center text-outline text-xs mt-4">Gracias por elegir Orion Fit Studio</p>
  `;
    UIService.openModal('receiptModal');
}

// ══════════════════════════════════════════════════════════════════════
// WOD CRUD
// ══════════════════════════════════════════════════════════════════════
function openWODModal(id = null) {
    document.getElementById('wodForm').reset();
    document.getElementById('wodId').value = '';
    document.getElementById('wodDate').value = new Date().toISOString().slice(0, 10);
    document.getElementById('exerciseList').innerHTML = '';
    addExerciseRow();
    addExerciseRow();
    addExerciseRow();

    if (id) {
        const w = Store.get('wods').find(w => w.id === id);
        if (!w) return;
        document.getElementById('wodModalTitle').textContent = 'Editar WOD';
        document.getElementById('wodId').value = w.id;
        document.getElementById('wodName').value = w.name;
        document.getElementById('wodDate').value = w.date;
        document.getElementById('wodCategory').value = w.category;
        document.getElementById('wodDesc').value = w.desc || '';
        document.getElementById('exerciseList').innerHTML = '';
        w.exercises.forEach(ex => addExerciseRow(ex));
    } else {
        document.getElementById('wodModalTitle').textContent = 'Publicar WOD';
    }
    UIService.openModal('wodModal');
}

function editWOD(id) { openWODModal(id); }

function addExerciseRow(data = {}) {
    const list = document.getElementById('exerciseList');
    const div = document.createElement('div');
    div.className = 'flex gap-2 items-center';
    div.innerHTML = `
    <input type="text" placeholder="Ejercicio *" class="input-field flex-1 py-2 text-sm ex-name" value="${data.name || ''}"/>
    <input type="text" placeholder="Series" class="input-field w-16 py-2 text-sm text-center ex-sets" value="${data.sets || ''}"/>
    <input type="text" placeholder="Reps" class="input-field w-16 py-2 text-sm text-center ex-reps" value="${data.reps || ''}"/>
    <input type="text" placeholder="Peso" class="input-field w-20 py-2 text-sm text-center ex-weight" value="${data.weight || ''}"/>
    <button type="button" class="btn-icon delete flex-shrink-0" onclick="this.parentNode.remove()">
      <span class="material-symbols-outlined text-[16px]">remove_circle</span>
    </button>`;
    list.appendChild(div);
}

document.getElementById('wodForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const exercises = [...document.querySelectorAll('#exerciseList > div')].map(row => ({
        name: row.querySelector('.ex-name').value.trim(),
        sets: row.querySelector('.ex-sets').value.trim(),
        reps: row.querySelector('.ex-reps').value.trim(),
        weight: row.querySelector('.ex-weight').value.trim(),
    })).filter(ex => ex.name);

    if (!exercises.length) { UIService.showToast('Agrega al menos un ejercicio', 'warning'); return; }

    const id = document.getElementById('wodId').value;
    const data = {
        name: document.getElementById('wodName').value.trim(),
        date: document.getElementById('wodDate').value,
        category: document.getElementById('wodCategory').value,
        desc: document.getElementById('wodDesc').value.trim(),
        exercises,
    };

    const wods = Store.get('wods');
    if (id) {
        const idx = wods.findIndex(w => w.id === parseInt(id));
        if (idx !== -1) wods[idx] = { ...wods[idx], ...data };
        UIService.showToast('WOD actualizado', 'success');
    } else {
        data.id = Store.getNextId('wods');
        wods.unshift(data);
        addNotification('Nuevo WOD publicado: ' + data.name, 'wod');
        UIService.showToast('WOD publicado exitosamente', 'success');
    }
    Store.set('wods', wods);
    UIService.closeModal('wodModal');
    Renderer.renderWODs();
});

function confirmDeleteWOD(id) {
    UIService.openConfirm('Eliminar WOD', '¿Eliminar este entrenamiento?', () => {
        Store.set('wods', Store.get('wods').filter(w => w.id !== id));
        Renderer.renderWODs();
        UIService.showToast('WOD eliminado', 'info');
    });
}

// ══════════════════════════════════════════════════════════════════════
// RESERVATIONS
// ══════════════════════════════════════════════════════════════════════
function toggleReservation(classId) {
    const classes = Store.get('classes');
    const idx = classes.findIndex(c => c.id === classId);
    if (idx === -1) return;
    const cls = classes[idx];

    if (cls.myReservation) {
        cls.myReservation = false;
        cls.booked--;
        addNotification('Reserva cancelada: ' + cls.name, 'reservation');
        UIService.showToast('Reserva cancelada', 'info');
    } else {
        if (cls.booked >= cls.max) { UIService.showToast('No hay cupos disponibles', 'error'); return; }
        cls.myReservation = true;
        cls.booked++;
        addNotification('Reserva confirmada: ' + cls.name, 'reservation');
        UIService.showToast('¡Reserva confirmada!', 'success');
    }
    Store.set('classes', classes);
    Renderer.renderReservations();
}

function openClassModal() { UIService.openModal('classModal'); }

document.getElementById('classForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const data = {
        id: Store.getNextId('classes'),
        name: document.getElementById('className').value.trim(),
        day: document.getElementById('classDay').value,
        time: document.getElementById('classTime').value,
        trainer: document.getElementById('classTrainer').value.trim() || 'Esteban Guevara',
        max: parseInt(document.getElementById('classMax').value) || 15,
        booked: 0,
        myReservation: false,
    };
    const classes = Store.get('classes');
    classes.push(data);
    Store.set('classes', classes);
    UIService.closeModal('classModal');
    Renderer.renderReservations();
    UIService.showToast('Clase creada exitosamente', 'success');
});

// ══════════════════════════════════════════════════════════════════════
// NOTIFICATIONS
// ══════════════════════════════════════════════════════════════════════
function addNotification(msg, type) {
    const notifs = Store.get('notifications');
    notifs.unshift({ id: Store.getNextId('notifications'), type, msg, time: 'Ahora mismo', read: false });
    Store.set('notifications', notifs);
    const count = notifs.filter(n => !n.read).length;
    const badge = document.getElementById('notifBadge');
    if (badge) { badge.textContent = count; badge.style.display = count > 0 ? 'flex' : 'none'; }
    const sideBadge = document.getElementById('notifBadgeSidebar');
    if (sideBadge) sideBadge.textContent = count;
}

function markNotifRead(id) {
    const notifs = Store.get('notifications');
    const n = notifs.find(n => n.id === id);
    if (n) { n.read = true; Store.set('notifications', notifs); Renderer.renderNotifications(); }
}

function markAllNotificationsRead() {
    const notifs = Store.get('notifications').map(n => ({ ...n, read: true }));
    Store.set('notifications', notifs);
    Renderer.renderNotifications();
    UIService.showToast('Todas las notificaciones marcadas como leídas', 'success');
}

// ══════════════════════════════════════════════════════════════════════
// INVENTORY CRUD
// ══════════════════════════════════════════════════════════════════════
function openInventoryModal(id = null) {
    document.getElementById('inventoryForm').reset();
    document.getElementById('inventoryId').value = '';
    document.getElementById('invDate').value = new Date().toISOString().slice(0, 10);

    if (id) {
        const item = Store.get('inventory').find(i => i.id === id);
        if (!item) return;
        document.getElementById('inventoryModalTitle').textContent = 'Editar Equipo';
        document.getElementById('inventoryId').value = item.id;
        document.getElementById('invName').value = item.name;
        document.getElementById('invCategory').value = item.category;
        document.getElementById('invStatus').value = item.status;
        document.getElementById('invQty').value = item.qty;
        document.getElementById('invMinStock').value = item.minStock;
        document.getElementById('invCost').value = item.cost;
        document.getElementById('invDate').value = item.acquired;
        document.getElementById('invDesc').value = item.desc;
    } else {
        document.getElementById('inventoryModalTitle').textContent = 'Registrar Equipo';
    }
    UIService.openModal('inventoryModal');
}

function editInventory(id) { openInventoryModal(id); }

document.getElementById('inventoryForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const id = document.getElementById('inventoryId').value;
    const qty = parseInt(document.getElementById('invQty').value);
    const minStock = parseInt(document.getElementById('invMinStock').value) || 0;
    const data = {
        name: document.getElementById('invName').value.trim(),
        category: document.getElementById('invCategory').value,
        status: document.getElementById('invStatus').value,
        qty, minStock,
        cost: document.getElementById('invCost').value,
        acquired: document.getElementById('invDate').value,
        desc: document.getElementById('invDesc').value.trim(),
        lastMaint: '—',
    };

    const inv = Store.get('inventory');
    if (id) {
        const idx = inv.findIndex(i => i.id === parseInt(id));
        if (idx !== -1) inv[idx] = { ...inv[idx], ...data };
        UIService.showToast('Equipo actualizado', 'success');
    } else {
        // Check duplicate name in category
        if (inv.find(i => i.name.toLowerCase() === data.name.toLowerCase() && i.category === data.category)) {
            UIService.showToast('Ya existe ese equipo en esa categoría', 'warning');
            return;
        }
        data.id = Store.getNextId('inventory');
        inv.push(data);
        UIService.showToast('Equipo registrado exitosamente', 'success');
    }
    Store.set('inventory', inv);
    UIService.closeModal('inventoryModal');
    Renderer.renderInventory();
});

function confirmDeleteInventory(id) {
    UIService.openConfirm('Eliminar Equipo', '¿Eliminar este equipo del inventario?', () => {
        Store.set('inventory', Store.get('inventory').filter(i => i.id !== id));
        Renderer.renderInventory();
        UIService.showToast('Equipo eliminado', 'info');
    });
}

function filterInventory() {
    const search = document.getElementById('invSearch').value;
    const status = document.getElementById('invStatusFilter').value;
    Renderer.renderInventory(search, status);
}

// ══════════════════════════════════════════════════════════════════════
// GLOBAL HELPERS (exposed to HTML onclick)
// ══════════════════════════════════════════════════════════════════════
function closeModal(id) { UIService.closeModal(id); }
function closeConfirm() { UIService.closeConfirm(); }
function executeConfirm() { UIService.executeConfirm(); }
function showToast(msg, type) { UIService.showToast(msg, type); }

function formatDate(dateStr) {
    if (!dateStr || dateStr === '—') return '—';
    try {
        return new Date(dateStr + 'T12:00:00').toLocaleDateString('es-CR', { day: '2-digit', month: 'short', year: 'numeric' });
    } catch { return dateStr; }
}

function getPlanPrice(plan) {
    const prices = { 'Mensual': '₡25,000', 'Trimestral': '₡65,000', 'Semestral': '₡120,000', 'Anual': '₡220,000' };
    return prices[plan] || '₡25,000';
}

// Global search
document.getElementById('globalSearch').addEventListener('input', function () {
    const section = Store.get('currentSection');
    if (section === 'clients') {
        document.getElementById('clientSearch').value = this.value;
        filterClients();
    } else if (section === 'inventory') {
        document.getElementById('invSearch').value = this.value;
        filterInventory();
    } else if (this.value.length > 1) {
        navigateTo('clients');
        setTimeout(() => {
            document.getElementById('clientSearch').value = this.value;
            filterClients();
        }, 100);
    }
});

// Close modals on overlay click
document.querySelectorAll('.modal-overlay').forEach(overlay => {
    overlay.addEventListener('click', function (e) {
        if (e.target === this) UIService.closeModal(this.id);
    });
});

// Sidebar navigation
document.querySelectorAll('.nav-item').forEach(el => {
    el.addEventListener('click', function () {
        navigateTo(this.dataset.section);
    });
});

// Init date fields
document.getElementById('reportStartDate').value = new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().slice(0, 10);
document.getElementById('reportEndDate').value = new Date().toISOString().slice(0, 10);


(function initWithoutLogin() {
    const defaultRole = 'admin';
    AuthService.login(defaultRole);
    const user = Store.get('currentUser');

    document.getElementById('mainApp').classList.add('flex'); // ya está visible

    // Actualizar UI con los datos del usuario (igual que antes)...
})();

// ════════════════════════════════════
// EQUIPO CRUD
// ════════════════════════════════════

function openEquipoModal(id = null) {
    document.getElementById('equipoForm').reset();
    document.getElementById('equipoId').value = '';

    if (id) {
        const equipo = Store.get('equipos')
            .find(e => e.id === id);

        if (!equipo) return;

        document.getElementById('equipoModalTitle')
            .textContent = 'Editar Equipo';

        document.getElementById('equipoId').value = equipo.id;
        document.getElementById('equipoNombre').value = equipo.nombre;
        document.getElementById('equipoDescripcion').value = equipo.descripcion || '';

    } else {
        document.getElementById('equipoModalTitle')
            .textContent = 'Agregar Equipo';
    }

    UIService.openModal('equipoModal');
}

function editEquipo(id) {
    openEquipoModal(id);
}

document.getElementById('equipoForm')
    .addEventListener('submit', function (e) {

        e.preventDefault();

        const id = document.getElementById('equipoId').value;

        const data = {
            nombre: document.getElementById('equipoNombre').value.trim(),
            descripcion: document.getElementById('equipoDescripcion').value.trim()
        };

        const equipos = Store.get('equipos');

        if (id) {
            const idx = equipos.findIndex(e => e.id === parseInt(id));

            if (idx !== -1) {
                equipos[idx] = {
                    ...equipos[idx],
                    ...data
                };
            }

            UIService.showToast('Equipo actualizado', 'success');

        } else {

            data.id = Store.getNextId('equipos');
            equipos.unshift(data);

            UIService.showToast('Equipo agregado', 'success');
        }

        Store.set('equipos', equipos);

        UIService.closeModal('equipoModal');

        Renderer.renderEquipos();
    });

function confirmDeleteEquipo(id) {
    UIService.openConfirm(
        'Eliminar equipo',
        '¿Eliminar este equipo?',
        () => {
            Store.set(
                'equipos',
                Store.get('equipos')
                    .filter(e => e.id !== id)
            );

            Renderer.renderEquipos();

            UIService.showToast(
                'Equipo eliminado',
                'info'
            );
        }
    );
}